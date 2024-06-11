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

        GameObject spawn1 = spawnRoom.GetComponent<Room>().spawns[0];

        GameObject spawn2 = mapRooms[2].spawns[0];
        GameObject spawn3 = mapRooms[3].spawns[0];

        Stack<Vector2Int> pathway = MapGrid.GeneratePath(new Vector2Int((int)spawn1.transform.position.x + 5, (int)spawn1.transform.position.y), new Vector2Int((int)spawn2.transform.position.x + 5, (int)spawn2.transform.position.y));

        if (pathway != null)
        {

            while (pathway.Count > 0)
            {
                //Debug.Log("(" + pathway.Peek().x + ", " + pathway.Peek().y + ")");

                Instantiate(GameManagerScript.instance.marker, (Vector3Int)pathway.Peek(), Quaternion.identity);
                pathway.Pop();

            }

        }

        pathway = MapGrid.GeneratePath(new Vector2Int((int)spawn1.transform.position.x + 5, (int)spawn1.transform.position.y), new Vector2Int((int)spawn3.transform.position.x + 5, (int)spawn3.transform.position.y));

        if (pathway != null)
        {

            while (pathway.Count > 0)
            {
                //Debug.Log("(" + pathway.Peek().x + ", " + pathway.Peek().y + ")");

                Instantiate(GameManagerScript.instance.marker, (Vector3Int)pathway.Peek(), Quaternion.identity);
                pathway.Pop();

            }

        }

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
