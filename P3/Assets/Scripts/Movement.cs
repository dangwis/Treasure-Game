using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public bool canMoveToPosition(Vector3 pos)
    {
        int roundX = Mathf.RoundToInt(pos.x);
        int roundY = Mathf.FloorToInt(pos.y);
        if (MapCreator.S.map[roundX, roundY].type == TileType.wall)
        {
            return false;
        }
        else return true;
    }
    public bool canMoveDownToPosition(Vector3 pos)
    {
        int roundX = Mathf.RoundToInt(pos.x);
        int roundY = Mathf.FloorToInt(pos.y);
        if (MapCreator.S.map[roundX, roundY].type == TileType.wall)
        {
            return false;
        }
        else return true;
    }
    public bool canMoveRightToPosition(Vector3 pos)
    {
        int roundX = Mathf.CeilToInt(pos.x);
        int roundY = Mathf.FloorToInt(pos.y);
        if (MapCreator.S.map[roundX, roundY].type == TileType.wall)
        {
            return false;
        }
        else return true;
    }
}
