using UnityEngine;
using System.Collections;

public class Ghosty : MonoBehaviour {

    public float startingSpeed;
    float movementSpeed;
    public float sanityIncreaseFirst;
    public float sanityIncreaseSecond;
    public float sanityDecreaseOnHit;
    public GameObject firePrefab;
    public GameObject sanityPrefab;
    public Sprite[] ghostySprites;

    SpriteRenderer spRend;
    
	// Use this for initialization
	void Start () {
        movementSpeed = startingSpeed;
        spRend = transform.Find("Sprite").GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        Vector2 playerPos = Player.S.transform.position;
        Vector2 myPos = transform.position;
        bool left, right, up;

        if(Player.S.sanity < sanityIncreaseFirst)
        {
            movementSpeed = startingSpeed * 1.5f;
        }
        if(Player.S.sanity < sanityIncreaseSecond)
        {
            movementSpeed = startingSpeed * 2.5f;
        }
        left = false;
        right = false;
        up = false;
        if (Mathf.Abs(playerPos.x - myPos.x) > 0.05f)
        {
            if (playerPos.x < myPos.x)
            {
                left = true;
                myPos.x -= movementSpeed * Time.fixedDeltaTime;
            }
            else if (playerPos.x > myPos.x)
            {
                right = true;
                myPos.x += movementSpeed * Time.fixedDeltaTime;
            }
        }
        if (Mathf.Abs(playerPos.y - myPos.y) > 0.05f)
        {
            if (playerPos.y < myPos.y)
            {
                myPos.y -= movementSpeed * Time.fixedDeltaTime;
            }
            else if (playerPos.y > myPos.y)
            {
                myPos.y += movementSpeed * Time.fixedDeltaTime;
                up = true;
            }
        }
        if (up)
        {
            spRend.sprite = ghostySprites[2];
        }
        else if (right)
        {
            spRend.sprite = ghostySprites[3];
        }
        else if (left)
        {
            spRend.sprite = ghostySprites[1];
        }
        else
        {
            spRend.sprite = ghostySprites[0];
        }
        transform.position = myPos;

        if (isPositionInLight(transform.position) && Vector2.Distance(playerPos, myPos) < 5f){
            GameObject fire = Instantiate(firePrefab);
            fire.transform.position = this.transform.position;
            Main.S.ghostburn.Play();
            Destroy(this.gameObject);
        }
	}

    public bool isPositionInLight(Vector2 pos)
    {
        int x = Mathf.RoundToInt(pos.x);
        int y = Mathf.RoundToInt(pos.y);
        if (MapCreator.S.map[x, y].lightAmount == TileLight.lit) return true;
        else return false;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject go = coll.gameObject;
        if(go.tag == "Player")
        {
            GameObject san = Instantiate(sanityPrefab);
            san.transform.position = Player.S.transform.position;
            Player.S.sanity -= sanityDecreaseOnHit;
            Destroy(this.gameObject);
        }
    }
}
