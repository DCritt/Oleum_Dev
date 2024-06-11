using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Tilemaps;
using System.IO;

public class MapGenerator : MonoBehaviour
{ 

    private Grid grid;
    [SerializeField] private Tilemap floor;
    [SerializeField] private Tilemap walls;
    [SerializeField] private Tilemap onFloor;

    [SerializeField] private float mapSize;

    private GameObject spawnRoom;

    [SerializeField] private SpawnPointPreset roomList;

    private GameObject[] rooms;
    private GameObject[] hallways;

    private Count roomAllowedAmount;
    private int roomAmt;

    private List<GameObject> activeMap = new List<GameObject>();
    private Queue<GameObject> roomRotationQueue = new Queue<GameObject>();


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

        //Generate(spawnRoom.GetComponent<Room>());
        GenerateMap();

    }

    public void AddRoomAmt(int amt)
    {

        roomAmt += amt;

    }

    public void SpawnObjectiveRooms(List<GameObject> objectiveRooms, ref List<Room> mapRooms)
    {

        for (int i = 0; i < objectiveRooms.Count; i++)
        {

            GameObject room = objectiveRooms[i];
            Room roomRoom = room.GetComponent<Room>();
            Vector2Int spawnPoint = new Vector2Int((Random.Range((int)-mapSize, (int)mapSize)), (Random.Range((int)-mapSize, (int)mapSize)));
            int spawnRotation = (90 * ((int)Random.Range(0, 4)));

            Debug.Log("rotation = " + spawnRotation);

            while (MapGrid.CheckRoomTiles(roomRoom, spawnPoint, spawnRotation))
            {

                spawnPoint.x = Random.Range((int)-mapSize, (int)mapSize);
                spawnPoint.y = Random.Range((int)-mapSize, (int)mapSize);
                spawnRotation = (90 * ((int)Random.Range(0, 4)));

            }

            roomRoom.rotation = spawnRotation;
            mapRooms.Add(Instantiate(room, (Vector3Int)spawnPoint, Quaternion.Euler(new Vector3Int(0, 0, spawnRotation)), grid.transform).GetComponent<Room>());
            MapGrid.AddRoomTiles(roomRoom, spawnPoint, spawnRotation);

        }

    }

    public void SpawnMapRooms(int roomAmt, ref List<Room> mapRooms)
    {

        for (int i = 0; i < roomAmt; i++)
        {

            GameObject room = roomList.GetRandList().GetRoom();
            Room roomRoom = room.GetComponent<Room>();
            Vector2Int spawnPoint = new Vector2Int((Random.Range((int)-mapSize, (int)mapSize)), (Random.Range((int)-mapSize, (int)mapSize)));
            int spawnRotation = (90 * ((int)Random.Range(0, 4)));

            while (MapGrid.CheckRoomTiles(roomRoom, spawnPoint, spawnRotation))
            {

                spawnPoint.x = Random.Range((int)-mapSize, (int)mapSize);
                spawnPoint.y = Random.Range((int)-mapSize, (int)mapSize);
                spawnRotation = (90 * ((int)Random.Range(0, 4)));

            }

            roomRoom.rotation = spawnRotation;
            mapRooms.Add(Instantiate(room, (Vector3Int)spawnPoint, Quaternion.Euler(new Vector3Int(0, 0, spawnRotation)), grid.transform).GetComponent<Room>());
            MapGrid.AddRoomTiles(roomRoom, spawnPoint, spawnRotation);

        }

    }

    public void GenerateMap()
    {

        spawnRoom = Instantiate(spawnRoom, new Vector3(0, 0, 0), grid.transform.rotation, grid.transform);
        MapGrid.AddTiles(new Vector2Int((spawnRoom.GetComponent<Room>().topOffset.x), (spawnRoom.GetComponent<Room>().topOffset.y)), new Vector2Int((spawnRoom.GetComponent<Room>().bottomOffset.x), (spawnRoom.GetComponent<Room>().bottomOffset.y)));
        List<Room> mapRooms = new List<Room>();

        SpawnObjectiveRooms(GameManagerScript.instance.mainObjective.mainRooms, ref mapRooms);
        SpawnObjectiveRooms(GameManagerScript.instance.mainObjective.sideRooms, ref mapRooms);

        SpawnMapRooms(8, ref mapRooms);

        for (int i = 0; i < mapRooms.Count; i++)
        {

            Room roomRoom = mapRooms[i];
            Room spawnRoomRoom = spawnRoom.GetComponent<Room>();

            Stack<Vector2Int> path = MapGrid.GeneratePath(MapGrid.GetRoomOpeningLocation(spawnRoomRoom, spawnRoomRoom.openings[0], spawnRoomRoom.rotation), MapGrid.GetRoomOpeningLocation(roomRoom, roomRoom.openings[0], roomRoom.rotation));

            while (path.Count > 0)
            {

                Instantiate(GameManagerScript.instance.marker, (Vector3Int)path.Pop(), Quaternion.identity, grid.transform);

            }

        }

        /*for (int i = 0; i < spawnRoom.GetComponent<Room>().openings.Length; i++)
        {

            Room spawnRoomRoom = spawnRoom.GetComponent<Room>();
            Room currRoom = mapRooms[i];

            for (int j = 0; j < currRoom.openings.Length; j++)
            {

                Debug.Log("j = " + j);

                Stack<Vector2Int> path = MapGrid.GeneratePath(MapGrid.GetRoomOpeningLocation(spawnRoomRoom, spawnRoomRoom.openings[i], spawnRoomRoom.rotation), MapGrid.GetRoomOpeningLocation(currRoom, currRoom.openings[j], currRoom.rotation));
                
                if (path != null)
                {

                    while (path.Count > 0)
                    {

                        Instantiate(GameManagerScript.instance.marker, (Vector3Int)path.Pop(), Quaternion.identity, grid.transform);

                    }

                }

            }

        }*/

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
