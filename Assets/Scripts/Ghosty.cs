using UnityEngine;
using System.Collections;

public class Ghosty : MonoBehaviour {

    public float startingSpeed;
    float movementSpeed;
    public float sanityIncreaseFirst;
    public float sanityIncreaseSecond;
    public float sanityDecreaseOnHit;
    
	// Use this for initialization
	void Start () {
        movementSpeed = startingSpeed;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        Vector2 playerPos = Player.S.transform.position;
        Vector2 myPos = transform.position;

        if(Player.S.sanity < sanityIncreaseFirst)
        {
            movementSpeed = startingSpeed * 1.5f;
        }
        if(Player.S.sanity < sanityIncreaseSecond)
        {
            movementSpeed = startingSpeed * 2.5f;
        }
	    if(playerPos.x < myPos.x)
        {
            myPos.x -= movementSpeed * Time.fixedDeltaTime;
        }
        else if(playerPos.x > myPos.x)
        {
            myPos.x += movementSpeed * Time.fixedDeltaTime;
        }
        if(playerPos.y < myPos.y)
        {
            myPos.y -= movementSpeed * Time.fixedDeltaTime;
        }
        else if(playerPos.y > myPos.y)
        {
            myPos.y += movementSpeed * Time.fixedDeltaTime;
        }

        transform.position = myPos;

        if (isPositionInLight(transform.position)){
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
            Player.S.sanity -= sanityDecreaseOnHit;
            Destroy(this.gameObject);
        }
    }
}
