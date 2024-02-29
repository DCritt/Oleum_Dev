using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{

    private Grid grid;

    private GameObject spawnRoom;

    private GameObject[] rooms;
    private GameObject[] hallways;

    private Count roomAllowedAmount;

    private Queue<GameObject> activeMap = new Queue<GameObject>();

    private void Awake()
    {



    }

    private void Start()
    {

      

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

        Generate(spawnRoom.GetComponent<Room>());

    }

    public void Generate(Room room)
    {

        GameObject temp;
        int runs = 0;

        while (roomAllowedAmount.CheckMax(runs)) {

            room.SetLists(rooms, hallways);

            for (int i = 0; i < room.spawns.Length; i++)
            {

                runs += 1;

                if (roomAllowedAmount.CheckMax(activeMap.Count))
                {

                    temp = room.spawn(i, grid);

                    if (temp != null)
                    {

                        activeMap.Enqueue(temp);

                    }

                }

            }

            Debug.Log(activeMap.Count);

            if (activeMap.Count != 0)
            {

                room = activeMap?.Dequeue().GetComponent<Room>();

            }

        }

        /*GameObject temp;

        temp = room.spawn(0, 0, hallways, grid);

        temp.GetComponent<Room>().spawn(0, 0, hallways, grid);

        temp = room.spawn(1, 0, hallways, grid);

        temp.GetComponent<Room>().spawn(0, 0, hallways, grid);*/

    }

}
