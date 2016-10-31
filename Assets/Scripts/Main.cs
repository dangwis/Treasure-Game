using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
    public static Main S;
    public GameObject ghostyPrefab;
    public float firstSpawnLevel, firstTimer;
    public float secondSpawnLevel, secondTimer;
    public float thirdSpawnLevel, thirdTimer;
    public float timeSinceLastSpawn = 0;

    public AudioSource fire;
    public AudioSource scream;
    public AudioSource footstep;
    public AudioSource heartbeat;
    public AudioSource money;
    public AudioSource ghostburn;
    public AudioSource main;
    public AudioSource intense;

	// Use this for initialization
	void Start () {
        S = this;
        timeSinceLastSpawn = 0;
        fire = fire.GetComponent<AudioSource>();
        scream = scream.GetComponent<AudioSource>();
        footstep = footstep.GetComponent<AudioSource>();
        heartbeat = heartbeat.GetComponent<AudioSource>();
        money = money.GetComponent<AudioSource>();
        ghostburn = ghostburn.GetComponent<AudioSource>();
        main = main.GetComponent<AudioSource>();
        intense = intense.GetComponent<AudioSource>();
	}

    public void SpawnGhosty(float time)
    {
        if(Player.S.sanity < thirdSpawnLevel)
        {
            if ((Time.time - timeSinceLastSpawn) >= thirdTimer)
            {
                timeSinceLastSpawn = Time.time;
                GameObject ghost = Instantiate(ghostyPrefab);
                ghost.transform.position = findPositionOnMap();
                scream.Play();
            }
        }
        else if(Player.S.sanity < secondSpawnLevel)
        {
            if((Time.time - timeSinceLastSpawn) >= secondTimer)
            {
                timeSinceLastSpawn = Time.time;
                GameObject ghost = Instantiate(ghostyPrefab);
                ghost.transform.position = findPositionOnMap();
                scream.Play();
            }
        }
        else if(Player.S.sanity < firstSpawnLevel)
        {
            if((Time.time - timeSinceLastSpawn) >= firstTimer)
            {
                timeSinceLastSpawn = Time.time;
                GameObject ghost = Instantiate(ghostyPrefab);
                ghost.transform.position = findPositionOnMap();
                scream.Play();
            }
        }
    }

    public Vector2 findPositionOnMap()
    {
        bool flag = false;
        int xPos = 0, yPos = 0;
        Vector2 ghostPos;
        while (!flag)
        {
            xPos = Mathf.RoundToInt(Random.Range(0, MapCreator.S.xSize));
            yPos = Mathf.RoundToInt(Random.Range(0, MapCreator.S.ySize));
            ghostPos = new Vector2(xPos, yPos);
            if (Vector2.Distance(Player.S.transform.position, ghostPos) > 3.5f)
            {
                if (MapCreator.S.map[xPos, yPos].lightAmount != TileLight.lit || MapCreator.S.map[xPos, yPos].lightAmount != TileLight.dim)
                {
                    flag = true;
                }
            }
        }
        return new Vector2(xPos, yPos);
    }


}
