using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Tilemaps;
using static UnityEditor.FilePathAttribute;

public class MapGenerator : MonoBehaviour
{

    private Grid grid;

    private GameObject spawnRoom;

    private GameObject[] rooms;
    private GameObject[] hallways;

    private Count roomAllowedAmount;

    private List<GameObject> activeMap = new List<GameObject>();

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

        int selectedRoom;
        int roomType;
        GameObject temp;

        room.SetLists(rooms, hallways);

        for (int i = 0; i < room.spawns.Length; i++)
        {

            roomType = Random.Range(0, 10);
            selectedRoom = Random.Range(0, GetRoomType(roomType).Length);
            if (roomAllowedAmount.CheckMax(activeMap.Count))
            {

                temp = room.spawn(i, selectedRoom, GetRoomType(roomType), grid);

                if (temp != null)
                {

                    activeMap.Add(temp);
                    Generate(activeMap[activeMap.Count - 1].GetComponent<Room>());

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
