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

    private List<GameObject> activeMap;

    private void Awake()
    {

    }

    private void Start()
    {

      

    }

    public GameObject[] GetRoomType(int i)
    {

        switch(i)
        {

            case 0:

                return rooms;
               
            case 1:

                return hallways;
                
            default:

                return null;
                

        }

    }

    public void SetGenerator(Grid grid, GameObject spawnroom, GameObject[] rooms, GameObject[] hallways)
    {

        this.grid = grid;
        this.spawnRoom = spawnroom;
        this.rooms = rooms;
        this.hallways = hallways;

        spawnRoom = UnityEngine.Object.Instantiate(spawnRoom, new Vector3(0, 0, 0), grid.transform.rotation, grid.transform);

        Generate(spawnRoom.GetComponent<Room>());

    }

    public void Generate(Room room)
    {

        int spawnChance;
        int selectedRoom;
        int roomType;

        for (int i = 0; i < room.spawns.Length; i++)
        {

            spawnChance = Random.Range(0, 20);
            if (spawnChance > 10)
            {

                selectedRoom = Random.Range(0, hallways.Length - 1);
                roomType = Random.Range(0, 2);

                Generate(room.spawn(i, selectedRoom, GetRoomType(roomType), grid).GetComponent<Room>());



            }

        }

        /*GameObject temp;

        temp = room.spawn(0, 0, hallways, grid);

        temp.GetComponent<Room>().spawn(0, 0, hallways, grid);

        temp = room.spawn(1, 0, hallways, grid);

        temp.GetComponent<Room>().spawn(0, 0, hallways, grid);*/

    }

}
