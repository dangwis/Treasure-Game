using UnityEngine;
using System.Collections;

public class Player : Movement {

    public static Player S;
    public Rigidbody2D rigid;
    public float movementSpeed = 1.5f;
    public int torches = 5;
    public float sanity = 1000f;
    public GameObject torchPrefab;
    public float timeSinceLastSpawn = 0;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();

        S = this;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector2 pos = transform.position;
        float moveTemp;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveTemp = movementSpeed * 2.5f;
        }
        else
        {
            moveTemp = movementSpeed;
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
            if(torches > 0)
            {
                GameObject torch = Instantiate(torchPrefab);
                torch.GetComponent<Torch>().PlaceTorch(transform.position);
                torches--;
            }
        }

        float santityTemp = sanityChange(transform.position);
        sanity += santityTemp;
        Main.S.SpawnGhosty(Time.time);
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
