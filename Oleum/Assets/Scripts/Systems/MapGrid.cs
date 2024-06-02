using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGrid
{


    private Dictionary<Vector2Int, bool> grid = new Dictionary<Vector2Int, bool>();

    public MapGrid()
    {

     

    }

    public void AddTile(Vector2Int tile)
    {

        grid.Add(tile, true);

    }

    public void AddTiles(Vector2Int top, Vector2Int bottom)
    {

        for (int i = top.y; i >= bottom.y; i--)
        {

            Debug.Log(i);

            for (int j = top.x; j <= bottom.x; j++)
            {

                Debug.Log(j);

                AddTile(new Vector2Int(j, i));

            }

        }
        
    }

    public bool CheckTile(Vector2Int tile)
    {

        return grid.ContainsKey(tile);

    }

    public bool CheckTiles(Vector2Int top, Vector2Int bottom)
    {

        for (int i = top.y; i >= bottom.y; i--)
        {

            for (int j = top.x; j <= bottom.x; j++)
            {

                if (CheckTile(new Vector2Int(j, i)))
                {

                    return true;

                }

            }

        }

        return false;

    }

}
