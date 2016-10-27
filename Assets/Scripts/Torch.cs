using UnityEngine;
using System.Collections;

public class Torch : MonoBehaviour
{

    public static Torch S;

    public int radius;
    public int outskirtSize;
    public bool inGame;

    public void Awake()
    {
        S = this;
    }

    void Start()
    {
        if (!inGame)
        {
            ActivateLights();
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
        
    }

    void ActivateLights()
    {
        for(int xVal = -radius - outskirtSize; xVal <= radius + outskirtSize; xVal++)
        {
            int currentX = (int)transform.position.x + xVal;
            if (currentX < 0 || currentX > MapCreator.S.xSize)
                continue; //Out of bounds
            for(int yVal = - radius; yVal <= radius; yVal++)
            {
                int currentY = (int)transform.position.y + yVal;
                if (currentY < 0 || currentY > MapCreator.S.ySize)
                    continue;
                if (xVal > radius && yVal > radius)
                    continue;
                else if (xVal > radius || yVal > radius)
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
        else if(amount == TileLight.dim)
        {
            MapCreator.S.map[(int)pos.x, (int)pos.y].lightAmount = TileLight.dim;
        }
        else
        {
            MapCreator.S.map[(int)pos.x, (int)pos.y].lightAmount = TileLight.black;
        }
        
    }
}
