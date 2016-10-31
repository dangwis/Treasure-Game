using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Torch : MonoBehaviour
{

    public int radius;
    public int outskirtSize;
    public bool inGame;
    public float lifespan, halflife, almostout;

    static List<Torch> torches;

    public Sprite[] torchSprites;
    SpriteRenderer spRend;
    Light lighting;
    int _torchIndex;
    float spawnTime;

    public void Awake()
    {
        spRend = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        lighting = transform.Find("Lighting").GetComponent<Light>();
        _torchIndex = 0;
        if(torches == null)
        {
            torches = new List<Torch>();
        }
    }

    void Start()
    {
        if (!inGame)
        {
            ActivateLights();
        }
        spawnTime = Time.time;
        torches.Add(this);
    }

    void FixedUpdate()
    {
        torchIndex++;
        if(Time.time - spawnTime > halflife)
        {
            lighting.intensity = 1.0f;
        }
        if(Time.time - spawnTime > almostout)
        {
            lighting.intensity = 0.5f;
        }
        if(Time.time - spawnTime > lifespan)
        {
            DeactivateLights();
            torches.Remove(this);
            for(int i = 0; i < torches.Count; i++)
            {
                if (Vector2.Distance(torches[i].transform.position, transform.position) <= 8f)
                {
                    torches[i].ActivateLights();
                }
            }
            Destroy(this.gameObject);
        }
    }

    public void PlaceTorch(Vector2 pos)
    {
        int xRound = Mathf.RoundToInt(pos.x);
        int yRound = Mathf.RoundToInt(pos.y);
        Vector2 newPos = new Vector2(xRound, yRound);
        this.transform.position = newPos;
        ActivateLights();
    }

    void DeactivateLights()
    {
        for (int xVal = -radius - outskirtSize; xVal <= radius + outskirtSize; xVal++)
        {
            int currentX = (int)transform.position.x + xVal;
            if (currentX < 0 || currentX >= MapCreator.S.xSize)
                continue; //Out of bounds
            for (int yVal = -radius; yVal <= radius; yVal++)
            {
                int currentY = (int)transform.position.y + yVal;
                if (currentY < 0 || currentY >= MapCreator.S.ySize)
                    continue;
                if (Math.Abs(xVal) > radius && Math.Abs(yVal) > radius)
                    continue;
                SetLightOfTile(new Vector2(currentX, currentY), TileLight.black);

            }
        }
    }

    void ActivateLights()
    {
        for (int xVal = -radius - outskirtSize; xVal <= radius + outskirtSize; xVal++)
        {
            int currentX = (int)transform.position.x + xVal;
            if (currentX < 0 || currentX >= MapCreator.S.xSize)
                continue; //Out of bounds
            for (int yVal = -radius; yVal <= radius; yVal++)
            {
                int currentY = (int)transform.position.y + yVal;
                if (currentY < 0 || currentY >= MapCreator.S.ySize)
                    continue;
                if (Math.Abs(xVal) > radius && Math.Abs(yVal) > radius)
                    continue;
                else if (Math.Abs(xVal) > radius || Math.Abs(yVal) > radius)
                {
                    if (MapCreator.S.map[currentX, currentY].lightAmount != TileLight.lit)
                    {
                        SetLightOfTile(new Vector2(currentX, currentY), TileLight.dim);
                    }
                }
                else
                    SetLightOfTile(new Vector2(currentX, currentY), TileLight.lit);

            }
        }
    }

    public void SetLightOfTile(Vector2 pos, TileLight amount)
    {
        if (amount == TileLight.lit)
        {
            MapCreator.S.map[(int)pos.x, (int)pos.y].lightAmount = TileLight.lit;
        }
        else if (amount == TileLight.dim)
        {
            MapCreator.S.map[(int)pos.x, (int)pos.y].lightAmount = TileLight.dim;
        }
        else
        {
            MapCreator.S.map[(int)pos.x, (int)pos.y].lightAmount = TileLight.black;
        }

    }

    public int torchIndex
    {
        get
        {
            return _torchIndex;
        }
        set
        {
            if (value % 12 == 0)
            {
                spRend.sprite = torchSprites[0];
            }
            else if (value % 12 == 4)
            {
                spRend.sprite = torchSprites[1];
            }
            else if (value % 12 == 8)
            {
                spRend.sprite = torchSprites[2];
            }
            _torchIndex = value;
        }
    }
}
