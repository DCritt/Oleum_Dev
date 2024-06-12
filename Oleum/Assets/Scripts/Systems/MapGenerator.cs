using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Tilemaps;
using System.IO;
using System.CodeDom.Compiler;
using Unity.VisualScripting;

public class MapGenerator : MonoBehaviour
{ 

    private Grid grid;
    [SerializeField] private Tilemap floor;
    [SerializeField] private Tilemap walls;
    [SerializeField] private Tilemap onFloor;
    [SerializeField] private Tilemap minimap;
    [SerializeField] private Tile floorTile;
    [SerializeField] private Tile minimapSquare;

    [SerializeField] private int mapSize;
    [SerializeField] private int roomNum;

    private GameObject spawnRoom;

    [SerializeField] private SpawnPointPreset roomList;

    private GameObject[] rooms;
    private GameObject[] hallways;

    private Count roomAllowedAmount;
    private int roomAmt;

    private List<GameObject> activeMap = new List<GameObject>();
    private Queue<GameObject> roomRotationQueue = new Queue<GameObject>();

    public void Start()
    { 

        MyList<int> list = new MyList<int>();
        list.InsertLast(0);
        list.InsertLast(1);
        list.InsertLast(2);

        list.RemoveAt(2);

        list.Print();

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

        //Generate(spawnRoom.GetComponent<Room>());
        GenerateMap();

    }

    public void AddRoomAmt(int amt)
    {

        roomAmt += amt;

    }

    public void SpawnObjectiveRooms(List<GameObject> objectiveRooms, ref List<Room> objRooms)
    {

        for (int i = 0; i < objectiveRooms.Count; i++)
        {

            GameObject room = objectiveRooms[i];
            Room roomRoom = room.GetComponent<Room>();
            Vector2Int spawnPoint = new Vector2Int(MyMath.RandRange2SidedGap((int)(mapSize * 1.5), (int)(mapSize / 2)), MyMath.RandRange2SidedGap((int)(mapSize * 1.5), (int)(mapSize / 2)));
            int spawnRotation = (90 * ((int)Random.Range(0, 4)));

            while (MapGrid.CheckRoomTiles(roomRoom, spawnPoint, spawnRotation))
            {

                spawnPoint.x = MyMath.RandRange2SidedGap((int)(mapSize * 1.5), (int)(mapSize / 2));
                spawnPoint.y = MyMath.RandRange2SidedGap((int)(mapSize * 1.5), (int)(mapSize / 2));
                spawnRotation = (90 * ((int)Random.Range(0, 4)));

            }

            roomRoom.rotation = spawnRotation;
            objRooms.Add(Instantiate(room, (Vector3Int)spawnPoint, Quaternion.Euler(new Vector3Int(0, 0, spawnRotation)), grid.transform).GetComponent<Room>());
            MapGrid.AddRoomTiles(roomRoom, spawnPoint, spawnRotation);

        }

    }

    public void SpawnMapRooms(int roomAmt, ref List<Room> mapRooms)
    {

        for (int i = 0; i < roomAmt; i++)
        {

            GameObject room = roomList.GetRandList().GetRoom();
            Room roomRoom = room.GetComponent<Room>();
            Vector2Int spawnPoint = new Vector2Int(MyMath.RandRange2SidedGap(mapSize, 10), MyMath.RandRange2SidedGap(mapSize, 10));

            int spawnRotation = (90 * ((int)Random.Range(0, 4)));

            while (MapGrid.CheckRoomTiles(roomRoom, spawnPoint, spawnRotation))
            {

                spawnPoint.x = MyMath.RandRange2SidedGap(mapSize, 10);
                spawnPoint.y = MyMath.RandRange2SidedGap(mapSize, 10);
                spawnRotation = (90 * ((int)Random.Range(0, 4)));

            }

            roomRoom.rotation = spawnRotation;
            mapRooms.Add(Instantiate(room, (Vector3Int)spawnPoint, Quaternion.Euler(new Vector3Int(0, 0, spawnRotation)), grid.transform).GetComponent<Room>());
            MapGrid.AddRoomTiles(roomRoom, spawnPoint, spawnRotation);

        }

    }

    public void AddFloorTiles(Tile tile, Vector2Int top, Vector2Int bottom)
    {

        for (int i = top.y; i > bottom.y; i--)
        {

            for (int j = top.x; j < bottom.x; j++)
            {

                floor.SetTile(new Vector3Int(j, i, 0), tile);
                minimap.SetTile(new Vector3Int(j, i, 0), minimapSquare);
                //Instantiate(GameManagerScript.instance.mapMarker, new Vector3Int(j, i, 0), Quaternion.identity, grid.transform);

            }

        }

    }

    public void SpawnHallwayTiles(MyStack<Vector2Int> path, int width)
    {

        while (path.size > 0)
        {

            Vector2Int top = new Vector2Int((path.Peek().x - width), (path.Peek().y + width));
            Vector2Int bottom = new Vector2Int((path.Peek().x + width), (path.Peek().y - width));

            AddFloorTiles(floorTile, top, bottom);
            path.Pop();

        }

    }

    public void SpawnMapHallways(ref List<Room> mapRooms)
    {

        Room spawnRoomRoom = spawnRoom.GetComponent<Room>();

        for (int i = 0; i < mapRooms.Count; i++)
        {

            Vector2Int loc = new Vector2Int((int)mapRooms[i].transform.position.x, (int)mapRooms[i].transform.position.y);
            List<int> possOpenings = new List<int>();

            if (loc.x < 0)
            {

                possOpenings.Add(0);

            }
            if (loc.x >= 0)
            {

                possOpenings.Add(1);

            }
            if (loc.y >= 0)
            {

                possOpenings.Add(2);

            }
            if (loc.y < 0)
            {

                possOpenings.Add(3);

            }

            int spawnRoomIndex = -1;
            int roomIndex = -1;

            if (possOpenings.Count > 1)
            {

                for (int j = 0; j < possOpenings.Count; j++)
                {

                    if (!spawnRoomRoom.openings[possOpenings[j]].used)
                    {

                        spawnRoomIndex = possOpenings[j];
                        j = possOpenings.Count;

                    }

                }

                if (spawnRoomIndex == -1)
                {

                    spawnRoomIndex = Random.Range(0, possOpenings.Count);

                }

            }
            else
            {

                spawnRoomIndex = 0;

            }

            MyStack<Vector2Int> path = null;
            int runs = 0;

            while (path == null && runs <= 4)
            {

                runs++;
                roomIndex = Random.Range(0, mapRooms[i].openings.Length);

                path = MapGrid.GeneratePath(MapGrid.GetRoomOpeningLocation(spawnRoomRoom, spawnRoomRoom.openings[spawnRoomIndex].loc, spawnRoomRoom.rotation), MapGrid.GetRoomOpeningLocation(mapRooms[i], mapRooms[i].openings[roomIndex].loc, mapRooms[i].rotation));

            }

            if (path != null)
            {

                spawnRoomRoom.openings[spawnRoomIndex].used = true;
                mapRooms[i].openings[roomIndex].used = true;

                SpawnHallwayTiles(path, 2);

            }


        }

    }

    public void GenerateMap()
    {
        
        spawnRoom = Instantiate(spawnRoom, new Vector3(0, 0, 0), grid.transform.rotation, grid.transform);
        MapGrid.AddTiles(new Vector2Int((spawnRoom.GetComponent<Room>().topOffset.x), (spawnRoom.GetComponent<Room>().topOffset.y)), new Vector2Int((spawnRoom.GetComponent<Room>().bottomOffset.x), (spawnRoom.GetComponent<Room>().bottomOffset.y)));
        List<Room> mapRooms = new List<Room>();
        List<Room> objRooms = new List<Room>();

        SpawnObjectiveRooms(GameManagerScript.instance.mainObjective.mainRooms, ref objRooms);
        SpawnObjectiveRooms(GameManagerScript.instance.mainObjective.sideRooms, ref objRooms);

        SpawnMapRooms(roomNum, ref mapRooms);

        SpawnMapHallways(ref mapRooms);

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
