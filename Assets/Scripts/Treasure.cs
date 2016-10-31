using UnityEngine;
using System.Collections;

public class Treasure : MonoBehaviour {

    public int scoreAdd;
    public float sanityAdd;
    public GameObject particlePrefab;

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
            Main.S.money.Play();
            Player.S.sanity += sanityAdd;
            Player.S.score += scoreAdd;
            GameObject particles = Instantiate(particlePrefab);
            Vector3 particlePos = transform.position;
            particlePos.z = -3;
            particles.transform.position = particlePos;
            Destroy(gameObject);
        }
    }
}
