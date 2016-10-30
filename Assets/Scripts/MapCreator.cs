using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public struct TileSprites
{
    public char identifier;
    public TileType type;
    public GameObject prefab;
    public GameObject item;
    public TileLight lightAmount;
}

public class MapCreator : MonoBehaviour
{
    public static MapCreator S;
    public List<TileSprites> tileSprites;
    public int xSize, ySize;
    public TextAsset textFile;

    public TileSprites[,] map;
    
    public void Start()
    {
        S = this;
        map = new TileSprites[xSize, ySize];
        DrawMap();
    }

    void DrawMap()
    {
        string input = textFile.text;
        string correctedData = "";
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] != '\r')
                correctedData = correctedData + input[i];
        }
        input = correctedData;
        int x = 0, y = 0, index = 0;
        while (index < input.Length)
        {
           char temp = input[index];
           map[x, y] = tileSprites[0];
           for(int i = 0; i < tileSprites.Count; i++)
           {
                if(temp == tileSprites[i].identifier)
                {
                    map[x, y] = tileSprites[i];
                    map[x, y].prefab = (GameObject)Instantiate(tileSprites[i].prefab, new Vector3(x, y, 0), Quaternion.identity);
                    if(tileSprites[i].item != null)
                    {
                        map[x, y].item = (GameObject)Instantiate(tileSprites[i].item, new Vector3(x, y, 0), Quaternion.identity);
                    }
                    break;
                }
            }
            if (map[x, y].prefab == null)
            {
                map[x, y].prefab = (GameObject)Instantiate(tileSprites[0].prefab, new Vector3(x, y, 0), Quaternion.identity);
            }
            index++;
            if (input[index] == '\n')
            {
                index++;
                y++;
                x = 0;
            }
            else x++;
        }
    }
}
