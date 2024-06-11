using JetBrains.Annotations;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

[System.Serializable]
public static class MapGrid
{

    private static Dictionary<Vector2Int, bool> grid = new Dictionary<Vector2Int, bool>();

    public static void AddTile(Vector2Int tile)
    {

        grid.Add(tile, true);
        UnityEngine.Object.Instantiate(GameManagerScript.instance.marker, new Vector3(tile.x, tile.y, -20), Quaternion.identity);

    }

    public static void AddTiles(Vector2Int top, Vector2Int bottom)
    {

        for (int i = top.y; i >= bottom.y; i--)
        {

            //Debug.Log(i);

            for (int j = top.x; j <= bottom.x; j++)
            {

                //Debug.Log(j);

                AddTile(new Vector2Int(j, i));

            }

        }
        
    }

    public static void AddRoomTiles(Room room, Vector2Int loc, int rot)
    {

        switch (rot)
        {

            case 0:

                AddTiles(new Vector2Int((loc.x + room.topOffset.x), (loc.y + room.topOffset.y)), new Vector2Int((loc.x + room.bottomOffset.x), (loc.y + room.bottomOffset.y)));
                break;

            case 90:

                AddTiles(new Vector2Int((loc.x - room.topOffset.y), (loc.y + room.bottomOffset.x)), new Vector2Int((loc.x - room.bottomOffset.y), (loc.y + room.topOffset.x)));
                break;

            case 180:

                AddTiles(new Vector2Int((loc.x - room.bottomOffset.x), (loc.y - room.bottomOffset.y)), new Vector2Int((loc.x - room.topOffset.x), (loc.y - room.topOffset.y)));
                break;

            case 270:

                AddTiles(new Vector2Int((loc.x + room.bottomOffset.y), (loc.y - room.topOffset.x)), new Vector2Int((loc.x + room.topOffset.y), (loc.y - room.bottomOffset.x)));
                break;

            default:

                return;

        }

    }

    public static bool CheckTile(Vector2Int tile)
    {

        return grid.ContainsKey(tile);

    }

    public static bool CheckTiles(Vector2Int top, Vector2Int bottom)
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

    public static bool CheckRoomTiles(Room room, Vector2Int loc, int rot)
    {

        switch(rot)
        {

            case 0:

                return (CheckTiles(new Vector2Int((loc.x + room.topOffset.x), (loc.y + room.topOffset.y)), new Vector2Int((loc.x + room.bottomOffset.x), (loc.y + room.bottomOffset.y))));

            case 90:

                return (CheckTiles(new Vector2Int((loc.x - room.topOffset.y), (loc.y + room.bottomOffset.x)), new Vector2Int((loc.x - room.bottomOffset.y), (loc.y + room.topOffset.x))));

            case 180:

                return (CheckTiles(new Vector2Int((loc.x - room.bottomOffset.x), (loc.y - room.bottomOffset.y)), new Vector2Int((loc.x - room.topOffset.x), (loc.y - room.topOffset.y))));

            case 270:

                return (CheckTiles(new Vector2Int((loc.x + room.bottomOffset.y), (loc.y - room.topOffset.x)), new Vector2Int((loc.x + room.topOffset.y), (loc.y - room.bottomOffset.x))));

            default:

                return false;

        }

    }

    public static void RemoveTile(Vector2Int tile)
    {

        if (CheckTile(tile))
        {

            grid.Remove(tile);

        }

    }

    public static void RemoveTiles(Vector2Int top, Vector2Int bottom)
    {

        for (int i = top.y; i >= bottom.y; i--)
        {

            for (int j = top.x;j <= bottom.x; j++)
            {

                RemoveTile(new Vector2Int(j, i));

            }

        }

    }

    public static Stack<Vector2Int> GeneratePath(Vector2Int start, Vector2Int end)
    {

        Dictionary<Vector2Int, List<Vector2Int>> nbrs = new Dictionary<Vector2Int, List<Vector2Int>>();
        Dictionary<Vector2Int, bool> visited = new Dictionary<Vector2Int, bool>();
        Dictionary<Vector2Int, Vector2Int> pred = new Dictionary<Vector2Int, Vector2Int>();
        Dictionary<Vector2Int, int> dist = new Dictionary<Vector2Int, int>();

        bool success = false;
        Queue<Vector2Int> pointList = new Queue<Vector2Int>();
        pointList.Enqueue(start);
        int runs = 0;
        Dictionary<Vector2Int, bool> used = new Dictionary<Vector2Int, bool>();

        while (!success && pointList.Count != 0)
        {

            Vector2Int currPos = pointList.Dequeue();
            bool addedNbrs = false;
            runs += 1;

            //Debug.Log("some (" + currPos.x + ", " + currPos.y + ")");
            //Debug.Log("Count = " + pointList.Count);

            nbrs[currPos] = new List<Vector2Int>();

            if (currPos == end)
            {

                success = true;

            }

            if ((currPos.y < end.y) && !used.ContainsKey(new Vector2Int(currPos.x, currPos.y + 1)) && !CheckTiles(new Vector2Int(currPos.x - 4, currPos.y + 5), new Vector2Int(currPos.x + 4, currPos.y + 1)))
            {

                //Debug.Log("up");

                addedNbrs = true;
                Vector2Int up = new Vector2Int(currPos.x, currPos.y + 1);

                nbrs[currPos].Add(up);
                pointList.Enqueue(up);
                used[up] = true;

            }

            if ((currPos.y > end.y) && !used.ContainsKey(new Vector2Int(currPos.x, currPos.y - 1)) && !CheckTiles(new Vector2Int(currPos.x - 4, currPos.y - 1), new Vector2Int(currPos.x + 4, currPos.y - 5)))
            {

                //Debug.Log("down");

                addedNbrs = true;
                Vector2Int down = new Vector2Int(currPos.x, currPos.y - 1);

                nbrs[currPos].Add(down);
                pointList.Enqueue(down);
                used[down] = true;

            }

            if ((currPos.x > end.x) && !used.ContainsKey(new Vector2Int(currPos.x - 1, currPos.y)) && !CheckTiles(new Vector2Int(currPos.x - 5, currPos.y + 4), new Vector2Int(currPos.x - 1, currPos.y - 4)))
            {

                //Debug.Log("left");

                addedNbrs = true;
                Vector2Int left = new Vector2Int(currPos.x - 1, currPos.y);

                nbrs[currPos].Add(left);
                pointList.Enqueue(left);
                used[left] = true;

            }

            if ((currPos.x < end.x) && !used.ContainsKey(new Vector2Int(currPos.x + 1, currPos.y)) && !CheckTiles(new Vector2Int(currPos.x + 1, currPos.y + 4), new Vector2Int(currPos.x + 5, currPos.y - 4)))
            {

                //Debug.Log("right");

                addedNbrs = true;
                Vector2Int right = new Vector2Int(currPos.x + 1, currPos.y);

                nbrs[currPos].Add(right);
                pointList.Enqueue(right);
                used[right] = true;

            }

            if (runs > 1000000)
            {

                pointList.Clear();

            }

            //Debug.Log(runs);

            if (!addedNbrs && runs < 1000000)
            {

                //Debug.Log("none (" + currPos.x + ", " + currPos.y + ")");

                if (CheckTiles(new Vector2Int(currPos.x - 5, currPos.y + 4), new Vector2Int(currPos.x - 1, currPos.y - 4)) || CheckTiles(new Vector2Int(currPos.x + 1, currPos.y + 4), new Vector2Int(currPos.x + 5, currPos.y - 4))) {

                    if (!used.ContainsKey(new Vector2Int(currPos.x, currPos.y + 1)) && !CheckTiles(new Vector2Int(currPos.x - 4, currPos.y + 5), new Vector2Int(currPos.x + 4, currPos.y + 1)))
                    {

                        //Debug.Log("up");

                        Vector2Int up = new Vector2Int(currPos.x, currPos.y + 1);

                        nbrs[currPos].Add(up);
                        pointList.Enqueue(up);
                        used[up] = true;

                    }

                    if (!used.ContainsKey(new Vector2Int(currPos.x, currPos.y - 1)) && !CheckTiles(new Vector2Int(currPos.x - 4, currPos.y - 1), new Vector2Int(currPos.x + 4, currPos.y - 5)))
                    {

                        //Debug.Log("down");

                        Vector2Int down = new Vector2Int(currPos.x, currPos.y - 1);

                        nbrs[currPos].Add(down);
                        pointList.Enqueue(down);
                        used[down] = true;

                    }

                }

                if (CheckTiles(new Vector2Int(currPos.x - 4, currPos.y + 5), new Vector2Int(currPos.x + 4, currPos.y + 1)) || CheckTiles(new Vector2Int(currPos.x - 4, currPos.y - 1), new Vector2Int(currPos.x + 4, currPos.y - 5))) {

                    if (!used.ContainsKey(new Vector2Int(currPos.x - 1, currPos.y)) && !CheckTiles(new Vector2Int(currPos.x - 5, currPos.y + 4), new Vector2Int(currPos.x - 1, currPos.y - 4)))
                    {

                        //Debug.Log("left");

                        Vector2Int left = new Vector2Int(currPos.x - 1, currPos.y);

                        nbrs[currPos].Add(left);
                        pointList.Enqueue(left);
                        used[left] = true;

                    }

                    if (!used.ContainsKey(new Vector2Int(currPos.x + 1, currPos.y)) && !CheckTiles(new Vector2Int(currPos.x + 1, currPos.y + 4), new Vector2Int(currPos.x + 5, currPos.y - 4)))
                    {

                        //Debug.Log("right");

                        Vector2Int right = new Vector2Int(currPos.x + 1, currPos.y);

                        nbrs[currPos].Add(right);
                        pointList.Enqueue(right);
                        used[right] = true;

                    }

                }

            }

        }

        Queue<Vector2Int> toVisit = new Queue<Vector2Int>();
        toVisit.Enqueue(start);
        visited[start] = true;
        dist[start] = 0;

        while (toVisit.Count != 0)
        {

            Vector2Int curr = toVisit.Dequeue();

            if (nbrs.ContainsKey(curr)) { 

                foreach (Vector2Int n in nbrs[curr])
                {

                    if (!visited.ContainsKey(n))
                    {


                        pred[n] = curr;
                        dist[n] = 1 + dist[curr];
                        visited[n] = true;
                        toVisit.Enqueue(n);

                    }

            }   
                
        }
            
     }

        if (pred.ContainsKey(end))
        {

            Debug.Log("distance" + dist[end]);

            Stack<Vector2Int> path = new Stack<Vector2Int>();
            Vector2Int temp = end;
            path.Push(temp);
            while (temp != start)
            {

                temp = pred[temp];
                path.Push(temp);

            }

            return path;

        }
        else
        {

            return null;

        }


        /*while (path.Count > 0)
        {

            Debug.Log("(" + path.Peek().x + ", " + path.Peek().y + ")");
            path.Pop();

        }*/

    }

}
