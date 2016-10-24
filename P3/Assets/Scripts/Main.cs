using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
    public static Main S;
    public GameObject ghostyPrefab;
    public float firstSpawnLevel;
    public float secondSpawnLevel;
    public float thirdSpawnLevel;

	// Use this for initialization
	void Start () {
        S = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SpawnGhosty(float sanity)
    {
        float randomVal = Random.Range(0, 100);
        if(sanity < thirdSpawnLevel)
        {
            if (randomVal <= 5)
            {
                GameObject ghost = Instantiate(ghostyPrefab);
                ghost.transform.position = findPositionOnMap();
            }
        }
        else if(sanity < secondSpawnLevel)
        {
            if(randomVal <= 2)
            {
                GameObject ghost = Instantiate(ghostyPrefab);
                ghost.transform.position = findPositionOnMap();
            }
        }
        else if(sanity < thirdSpawnLevel)
        {
            if(randomVal <= 0.5)
            {
                GameObject ghost = Instantiate(ghostyPrefab);
                ghost.transform.position = findPositionOnMap();
            }
        }
    }

    public Vector2 findPositionOnMap()
    {
        bool flag = false;
        int xPos = 0, yPos = 0;
        while (!flag)
        {
            xPos = Mathf.RoundToInt(Random.Range(0, MapCreator.S.xSize));
            yPos = Mathf.RoundToInt(Random.Range(0, MapCreator.S.ySize));
            if(MapCreator.S.map[xPos, yPos].lightAmount != TileLight.lit || MapCreator.S.map[xPos, yPos].lightAmount != TileLight.dim)
            {
                flag = true;
            }
        }
        return new Vector2(xPos, yPos);
    }


}
