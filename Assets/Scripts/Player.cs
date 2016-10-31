using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : Movement {

    public static Player S;
    public Rigidbody2D rigid;
    public float movementSpeed = 1.5f;
    public int torches = 5;
    public float sanity = 1000f;
    public GameObject torchPrefab;
    public float timeSinceLastSpawn = 0;
    public int score;
    public float stamina = 100f;
    public float staminaRegenTimer = 2f;
    float staminaReleaseTime;
    bool gameOver;
    bool placingTorch;
    public int betweenFootsteps;
    int footLeft;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
        score = 0;
        gameOver = false;
        S = this;
        placingTorch = false;
        footLeft = 0;
	}
	
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            placingTorch = true;
        }
        if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) 
            || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (footLeft <= 0)
            {
                Main.S.footstep.Play();
                Main.S.footstep.loop = false;
                footLeft = betweenFootsteps;
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    footLeft = footLeft - 2;
                }
                else
                {
                    footLeft--;
                }
            }
        }
    }

	// Update is called once per frame
	void FixedUpdate () {
        Vector2 pos = transform.position;
        float moveTemp;
        if (gameOver)
        {
            if (PlayerPrefs.HasKey("firstPlace"))
            {
                if (PlayerPrefs.GetFloat("firstPlace") < score)
                {
                    PlayerPrefs.SetFloat("thirdPlace", PlayerPrefs.GetFloat("secondPlace"));
                    PlayerPrefs.SetFloat("secondPlace", PlayerPrefs.GetFloat("firstPlace"));
                    PlayerPrefs.SetFloat("firstPlace", score);
                    PlayerPrefs.SetString("justRanked", "First");
                }
                else if(PlayerPrefs.GetFloat("secondPlace") < score)
                {
                    PlayerPrefs.SetFloat("thirdPlace", PlayerPrefs.GetFloat("secondPlace"));
                    PlayerPrefs.SetFloat("secondPlace", score);
                    PlayerPrefs.SetString("justRanked", "Second");
                }
                else if(PlayerPrefs.GetFloat("thirdPlace") < score)
                {
                    PlayerPrefs.SetFloat("thirdPlace", score);
                    PlayerPrefs.SetString("justRanked", "Third");
                }
                else
                {
                    PlayerPrefs.SetString("justRanked", "N/A");
                }
            }
            else
            {
                PlayerPrefs.SetFloat("firstPlace", score);
                PlayerPrefs.SetFloat("secondPlace", 0);
                PlayerPrefs.SetFloat("thirdPlace", 0);
            }
            PlayerPrefs.SetInt("justScored", score);
            PlayerPrefs.Save();
            SceneManager.LoadScene("_scene_end");
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftShift) && 
                (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || 
                    Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)))
            {
                if (stamina > 0)
                {
                    stamina -= 0.25f;
                    moveTemp = movementSpeed * 2.5f;
                    staminaReleaseTime = Time.time;
                }
                else
                {
                    moveTemp = movementSpeed;
                    staminaReleaseTime = Time.time;
                }
            }
            else
            {
                moveTemp = movementSpeed;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                staminaReleaseTime = Time.time;
            }

            if (Time.time - staminaReleaseTime >= staminaRegenTimer && stamina <= 100)
            {
                stamina += 0.5f;
                //Sometimes I wonder if it's even worth catching my breath. Death looms over us all and it calls to me like a siren call beckoning me to come home
                // Also this restores stamina of the character. 
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                pos.y += moveTemp * Time.fixedDeltaTime;
                if (canMoveToPosition(pos))
                {
                    transform.position = pos;
                }
                else
                {
                    pos = transform.position;
                }
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                pos.y -= moveTemp * Time.fixedDeltaTime;
                if (canMoveDownToPosition(pos))
                {
                    transform.position = pos;
                }
                else
                {
                    pos = transform.position;
                }
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                pos.x += moveTemp * Time.fixedDeltaTime;
                if (canMoveRightToPosition(pos))
                {
                    transform.position = pos;
                }
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                pos.x -= moveTemp * Time.fixedDeltaTime;
                if (canMoveToPosition(pos))
                {
                    transform.position = pos;
                }
            }

            if (placingTorch)
            {
                if (torches > 0)
                {
                    GameObject torch = Instantiate(torchPrefab);
                    torch.GetComponent<Torch>().PlaceTorch(transform.position);
                    torches--;
                }
                placingTorch = false;
            }

            float santityTemp = sanityChange(transform.position);
            sanity += santityTemp;
            if (sanity <= 600)
            {
                if (!Main.S.heartbeat.isPlaying)
                {
                    Main.S.heartbeat.Play();
                    Main.S.main.volume = 0.35f;
                    Main.S.heartbeat.loop = true;
                }
            }
            if(sanity <= 300)
            {
                Main.S.main.Stop();
                Main.S.intense.Play();
                Main.S.heartbeat.volume = 1.0f;
            }
            else
            {
                Main.S.heartbeat.Stop();
            }

            if (sanity <= 0)
            {
                gameOver = true;
            }
            Main.S.SpawnGhosty(Time.time);
        }
    }

    bool NotInTheLight()
    {
        int x = Mathf.RoundToInt(transform.position.x);
        int y = Mathf.RoundToInt(transform.position.y);
        if (MapCreator.S.map[x, y].lightAmount == TileLight.black || MapCreator.S.map[x, y].lightAmount == TileLight.dim) return true;
        else return false;

    }

    public float sanityChange(Vector2 pos)
    {
        int x = Mathf.RoundToInt(pos.x);
        int y = Mathf.RoundToInt(pos.y);
        if(MapCreator.S.map[x,y].lightAmount == TileLight.black)
        {
            if (Main.S.fire.isPlaying)
            {
                Main.S.fire.Stop();
            }
            TheDarkness.S.SetDarkness(TileLight.black);
            return -0.5f;
        }
        else if(MapCreator.S.map[x, y].lightAmount == TileLight.dim)
        {
            if (!Main.S.fire.isPlaying)
            {
                Main.S.fire.Play();
                Main.S.fire.loop = true;
            }
            TheDarkness.S.SetDarkness(TileLight.dim);
            return -0.25f;
        }
        else
        {
            if (!Main.S.fire.isPlaying)
            {
                Main.S.fire.Play();
                Main.S.fire.loop = true;
            }
            TheDarkness.S.SetDarkness(TileLight.lit);
            return 0f;
        }
    }
}
