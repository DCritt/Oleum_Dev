using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{ 

    private Grid grid;

    public MapGrid map;

    private GameObject spawnRoom;

    private GameObject[] rooms;
    private GameObject[] hallways;

    private Count roomAllowedAmount;
    private int roomAmt;

    private List<GameObject> activeMap = new List<GameObject>();
    private Queue<GameObject> roomRotationQueue = new Queue<GameObject>();

    private void Awake()
    {

        map = new MapGrid();

    }

    private void Start()
    {

        //map.AddTiles(new Vector2Int(0, 0), new Vector2Int(1000, -1000));

    }

    public GameObject[] GetRoomType(int i)
    {

        if (i > 3)
        {

            return hallways;

        }
        else
        {

            return rooms;

        }

    }

    public void SetGenerator(Grid grid, GameObject spawnroom, GameObject[] rooms, GameObject[] hallways, Count roomAllowedAmount)
    {

        this.grid = grid;
        this.spawnRoom = spawnroom;
        this.rooms = rooms;
        this.hallways = hallways;
        this.roomAllowedAmount = new Count(roomAllowedAmount.GetMin(), roomAllowedAmount.GetMax());

        spawnRoom = UnityEngine.Object.Instantiate(spawnRoom, new Vector3(0, 0, 0), grid.transform.rotation, grid.transform);

        //Generate(spawnRoom.GetComponent<Room>());

    }

    public void AddRoomAmt(int amt)
    {

        roomAmt += amt;

    }

    public void Generate(Room room)
    {
        //hello
        GameObject temp;

        int runs = 0;

        while (roomAllowedAmount.CheckMax(roomAmt) && runs < 400) {

            room.SetMapGen(this);

            runs += 1;

            for (int i = 0; i < room.spawns.Length; i++)
            {

                if (roomAllowedAmount.CheckMax(roomRotationQueue.Count))
                {

                    temp = room.spawn(i, grid);

                    if (temp != null)
                    {

                        roomRotationQueue.Enqueue(temp);
                        activeMap.Add(temp);

                    }

                }

            }

            if (roomRotationQueue.Count != 0)
            {

                //activeMap.Enqueue(activeMap.Peek());
                room = roomRotationQueue?.Dequeue().GetComponent<Room>();

            }

        }

        for(int i = 0; i < activeMap.Count; i++)
        {

            for (int j = 0; j < activeMap[i].GetComponent<Room>().spawns.Length; j++)
            {

                if (activeMap[i].GetComponent<Room>().spawns[j].GetComponent<SpawnPoint>().GetState())
                {

                    activeMap.Add(activeMap[i].GetComponent<Room>().CheckRays(j, grid, true));

                }

            }


        }

        /*GameObject temp;

        temp = room.spawn(0, 0, hallways, grid);

        temp.GetComponent<Room>().spawn(0, 0, hallways, grid);

        temp = room.spawn(1, 0, hallways, grid);

        temp.GetComponent<Room>().spawn(0, 0, hallways, grid);*/

    }

}
