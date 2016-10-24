using UnityEngine;
using System.Collections;

public class Torch : MonoBehaviour
{

    public static Torch S;

    public int radius;
    public int outskirtSize;
    public bool inGame;

    Vector2 lastPos;

    int arraySize;
    int[,] lightValues;

    public void Awake()
    {
        arraySize = (radius) * 2 + 1; //Left(r) + Right(r) + Player(1)
        lightValues = new int[arraySize, arraySize];
        S = this;
    }

    void Start()
    {
        if (!inGame)
        {
            lastPos = this.transform.position;
            ActivateLights();
        }
    }

    public void PlaceTorch(Vector2 pos)
    {
        int xRound = Mathf.RoundToInt(pos.x);
        int yRound = Mathf.RoundToInt(pos.y);
        Vector2 newPos = new Vector2(xRound, yRound);
        lastPos = newPos;
        this.transform.position = newPos;
        ActivateLights();
    }

    void DeactivateLights()
    {
        for (int yIndex = 0; yIndex < arraySize; yIndex++)
        {
            int yReal = yIndex - radius - 1 + (int)lastPos.y;
            if (yReal < 0 || yReal >= MapCreator.S.ySize)
                continue; // Out of bounds
            for (int xIndex = 0; xIndex < arraySize; xIndex++)
            {
                int xReal = xIndex - radius - 1 + (int)lastPos.x;
                if (xReal < 0 || xReal >= MapCreator.S.xSize)
                    continue; // Out of bounds

                SetLightOfTile(new Vector2(xReal, yReal), TileLight.black);
            }
        }

        for (int yIndex = 0; yIndex < arraySize; yIndex++)
        {
            for (int xIndex = 0; xIndex < arraySize; xIndex++)
            {
                // Reset light matrix
                lightValues[xIndex, yIndex] = 0;
            }
        }
    }

    void ActivateLights()
    {

        lightValues[radius + 1, radius + 1] = radius; //Light player
        SpreadLightAroundPos(new Vector2(radius + 1, radius + 1), 1); //Light around player

        GenerateLightMatrix();

        LightLevel();
    }

    void SpreadLightAroundPos(Vector2 pos, int lightDecrease)
    {
        // Note: This function executes only on the local matrix
        int spreadLightAmount = lightValues[(int)pos.x, (int)pos.y] - lightDecrease;

        if (pos.x - 1 >= 0) // Left
            lightValues[(int)pos.x - 1, (int)pos.y] = Mathf.Max(lightValues[(int)pos.x - 1, (int)pos.y], spreadLightAmount);
        if (pos.x + 1 < arraySize) // Right
            lightValues[(int)pos.x + 1, (int)pos.y] = Mathf.Max(lightValues[(int)pos.x + 1, (int)pos.y], spreadLightAmount);
        if (pos.y + 1 < arraySize) // Up
            lightValues[(int)pos.x, (int)pos.y + 1] = Mathf.Max(lightValues[(int)pos.x, (int)pos.y + 1], spreadLightAmount);
        if (pos.y - 1 >= 0) // Down
            lightValues[(int)pos.x, (int)pos.y - 1] = Mathf.Max(lightValues[(int)pos.x, (int)pos.y - 1], spreadLightAmount);
    }

    void GenerateLightMatrix()
    {
        //TODO possibly optimize with another matrix of bools to determine which tiles still need to be done (or do recursively)
        for (int step = 1; step < radius; step++) // Run one time less than the radius (No need to spread 1 value lights)
        {
            for (int yIndex = 0; yIndex < arraySize; yIndex++)
            {
                int yReal = yIndex - radius - 1 + (int)lastPos.y;
                if (yReal < 0 || yReal >= MapCreator.S.ySize)
                    continue; // Out of bounds
                for (int xIndex = 0; xIndex < arraySize; xIndex++)
                {
                    int xReal = xIndex - radius - 1 + (int)lastPos.x;
                    if (xReal < 0 || xReal >= MapCreator.S.xSize)
                        continue; // Out of bounds
                    SpreadLightAroundPos(new Vector2(xIndex, yIndex), 1);
                }
            }
        }
    }

    void LightLevel()
    {
        for (int yIndex = 0; yIndex < arraySize; yIndex++)
        {
            int yReal = yIndex - radius - 1 + (int)lastPos.y;
            if (yReal < 0 || yReal >= MapCreator.S.ySize)
                continue; // Out of bounds
            for (int xIndex = 0; xIndex < arraySize; xIndex++)
            {
                int xReal = xIndex - radius - 1 + (int)lastPos.x;
                if (xReal < 0 || xReal >= MapCreator.S.xSize)
                    continue; // Out of bounds
                if (lightValues[xIndex, yIndex] > outskirtSize)
                    SetLightOfTile(new Vector2(xReal, yReal), TileLight.lit);
                else if (lightValues[xIndex, yIndex] > 0)
                    SetLightOfTile(new Vector2(xReal, yReal), TileLight.dim);
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
