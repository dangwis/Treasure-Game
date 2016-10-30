using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
        score = 0;
        gameOver = false;
        S = this;
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
                }
                else if(PlayerPrefs.GetFloat("secondPlace") < score)
                {
                    PlayerPrefs.SetFloat("thirdPlace", PlayerPrefs.GetFloat("secondPlace"));
                    PlayerPrefs.SetFloat("secondPlace", score);
                }
                else if(PlayerPrefs.GetFloat("thirdPlace") < score)
                {
                    PlayerPrefs.SetFloat("thirdPlace", score);
                }
            }
            else
            {
                PlayerPrefs.SetFloat("firstPlace", score);
                PlayerPrefs.SetFloat("secondPlace", 0);
                PlayerPrefs.SetFloat("thirdPlace", 0);
            }
            PlayerPrefs.Save();
            SceneManager.LoadScene("_scene_menu  ");
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftShift))
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

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (torches > 0)
                {
                    GameObject torch = Instantiate(torchPrefab);
                    torch.GetComponent<Torch>().PlaceTorch(transform.position);
                    torches--;
                }
            }

            float santityTemp = sanityChange(transform.position);
            sanity += santityTemp;
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
            return -0.5f;
        }
        else if(MapCreator.S.map[x, y].lightAmount == TileLight.dim)
        {
            return -0.25f;
        }
        else
        {
            return 0f;
        }
    }
}
