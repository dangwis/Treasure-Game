using UnityEngine;
using System.Collections;

public class Treasure : MonoBehaviour {

    public int scoreAdd;
    public float sanityAdd;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject go = coll.gameObject;
        if(go.tag == "Player")
        {
            Player.S.sanity += sanityAdd;
            Player.S.score += scoreAdd;
            Destroy(this.gameObject);
        }
    }
}
