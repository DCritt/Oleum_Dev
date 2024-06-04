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

    private struct priorityDirection
    {

        public Vector2Int direction;
        public int priority;

        public priorityDirection(Vector2Int direction, int priority)
        {

            this.direction = direction;
            this.priority = priority;

        }

    }

    private static Dictionary<Vector2Int, bool> grid = new Dictionary<Vector2Int, bool>();

    private static priorityDirection up = new priorityDirection(new Vector2Int(0, 1), 0);
    private static priorityDirection down = new priorityDirection(new Vector2Int(0, -1), 0);
    private static priorityDirection left = new priorityDirection(new Vector2Int(-1, 0), 0);
    private static priorityDirection right = new priorityDirection(new Vector2Int(1, 0), 0);

    private static priorityDirection GetDirection(int index)
    {

        switch(index)
        {

            case 0:

                return up;
                break;

            case 1:

                return down;
                break;

            case 2:

                return left;
                break;

            case 3:

                return right;
                break;

            default:

                return new priorityDirection(new Vector2Int(0, 0), 0);
                break;

        }

    }

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

    public static List<Vector2Int> GeneratePath(Vector2Int start, Vector2Int end)
    {

        List<Vector2Int> path = new List<Vector2Int>();

        Dictionary<Vector2Int, List<Vector2Int>> nbrs = new Dictionary<Vector2Int, List<Vector2Int>>();
        Dictionary<Vector2Int, bool> visited = new Dictionary<Vector2Int, bool>();
        Dictionary<Vector2Int, Vector2Int> pred = new Dictionary<Vector2Int, Vector2Int>();
        Dictionary<Vector2Int, int> dist = new Dictionary<Vector2Int, int>();

        bool success = false;
        Queue<Vector2Int> pointList = new Queue<Vector2Int>();
        pointList.Enqueue(start);

        while (!success && pointList.Count != 0)
        {

            Debug.Log(nbrs.Count);

            Debug.Log("hello");

            Vector2Int currPos = pointList.Dequeue();

            nbrs[currPos] = new List<Vector2Int>();

            if (currPos == end)
            {

                success = true;

            }

            if (!nbrs.ContainsKey(new Vector2Int(currPos.x, currPos.y + 1)) && !CheckTiles(new Vector2Int(currPos.x - 4, currPos.y + 4), new Vector2Int(currPos.x + 4, currPos.y + 1)) && (Mathf.Abs(start.y - currPos.y) < 50))
            {

                Vector2Int up = new Vector2Int(currPos.x, currPos.y + 1);

                nbrs[currPos].Add(up);
                pointList.Enqueue(up);

            }

            if (!nbrs.ContainsKey(new Vector2Int(currPos.x, currPos.y - 1)) && !CheckTiles(new Vector2Int(currPos.x - 4, currPos.y - 1), new Vector2Int(currPos.x + 4, currPos.y - 4)) && (Mathf.Abs(start.y - currPos.y) < 50))
            {

                Vector2Int down = new Vector2Int(currPos.x, currPos.y - 1);

                nbrs[currPos].Add(down);
                pointList.Enqueue(down);

            }

            if (!nbrs.ContainsKey(new Vector2Int(currPos.x - 1, currPos.y)) && !CheckTiles(new Vector2Int(currPos.x - 4, currPos.y + 4), new Vector2Int(currPos.x - 1, currPos.y - 4)) && (Mathf.Abs(start.x - currPos.x) < 50))
            {

                Vector2Int left = new Vector2Int(currPos.x - 1, currPos.y);

                nbrs[currPos].Add(left);
                pointList.Enqueue(left);

            }

            if (!nbrs.ContainsKey(new Vector2Int(currPos.x + 1, currPos.y)) && !CheckTiles(new Vector2Int(currPos.x + 1, currPos.y + 4), new Vector2Int(currPos.x + 4, currPos.y - 4)) && (Mathf.Abs(start.x - currPos.x) < 50))
            {

                Vector2Int right = new Vector2Int(currPos.x + 1, currPos.y);

                nbrs[currPos].Add(right);
                pointList.Enqueue(right);

            }

        }

        Debug.Log("here");

        Queue<Vector2Int> toVisit = new Queue<Vector2Int>();
        toVisit.Enqueue(start);
        visited[start] = true;
        dist[start] = 0;

        while (toVisit.Count != 0)
        {

            Debug.Log("while");

            Vector2Int curr = toVisit.Dequeue();

            if (nbrs.ContainsKey(curr)) { 

                foreach (Vector2Int n in nbrs[curr])
                {

                    Debug.Log("foreach");

                    if (!visited.ContainsKey(n))
                    {

                        Debug.Log("here");

                        pred[n] = curr;

                        Debug.Log("pred");

                        dist[n] = 1 + dist[curr];

                        Debug.Log("dist");
                        
                        visited[n] = true;

                        Debug.Log("visited");

                        toVisit.Enqueue(n); 
                        
                        Debug.Log("Enqueue");

                    }

            }   
                
        }
            
     }

        Debug.Log("distance" + dist[end]);

        Stack<Vector2Int> stack = new Stack<Vector2Int>();
        Vector2Int temp = end;
        stack.Push(temp);
        while (temp != start)
        {

            temp = pred[temp];
            stack.Push(temp);

        }


        while (stack.Count > 0)
        {

            Debug.Log("(" + stack.Peek().x + ", " + stack.Peek().y + ")");
            stack.Pop();

        }

        return path;

    }

}
