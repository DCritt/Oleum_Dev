using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Room : MonoBehaviour
{

    public GameObject[] spawns;
    private RandList chosenList;
    private MapGenerator mapGen;
    [SerializeField] private LayerMask room;

    [SerializeField] private Vector2Int topOffset;
    [SerializeField] private Vector2Int bottomOffset;

    public void Start()
    {

        MapGrid.AddTiles((new Vector2Int(((int)gameObject.transform.position.x + topOffset.x), ((int)gameObject.transform.position.y + topOffset.y))), (new Vector2Int(((int)gameObject.transform.position.x + bottomOffset.x), ((int)gameObject.transform.position.y + bottomOffset.y))));

    }

    public void SetMapGen(MapGenerator mapGen)
    {

        this.mapGen = mapGen;

    }

    public GameObject spawn(int index, Grid grid)
    {

        int spawnChance;

        int chanceThresh;

        chosenList = spawns[index].GetComponent<SpawnPoint>().preset.GetRandList();

        spawnChance = Random.Range(0, 100);

        chanceThresh = 100;

        mapGen.AddRoomAmt(chosenList.GetAmt());
        return CheckRays(index, grid, !(spawnChance < chanceThresh));

    }

    public GameObject CheckRays(int index, Grid grid, bool failed)
    {

        RaycastHit2D[] rayList = new RaycastHit2D[7];
        float distance = 0;

        spawns[index].GetComponent<SpawnPoint>().Deactivate();

        rayList[0] = Physics2D.Raycast(spawns[index].transform.TransformPoint(Vector2.right * .1f), spawns[index].transform.right, 20, room);
        rayList[1] = Physics2D.Raycast(spawns[index].transform.TransformPoint(Vector2.right * .1f), spawns[index].transform.TransformDirection(Vector2.right + (Vector2.up * .3f)), 20, room);
        rayList[2] = Physics2D.Raycast(spawns[index].transform.TransformPoint(Vector2.right * .1f), spawns[index].transform.TransformDirection(Vector2.right + (Vector2.up * -.3f)), 20, room);
        rayList[3] = Physics2D.Raycast(spawns[index].transform.TransformPoint(Vector2.right * .1f), spawns[index].transform.TransformDirection(Vector2.right + (Vector2.up * .6f)), 20, room);
        rayList[4] = Physics2D.Raycast(spawns[index].transform.TransformPoint(Vector2.right * .1f), spawns[index].transform.TransformDirection(Vector2.right + (Vector2.up * -.6f)), 20, room);
        rayList[5] = Physics2D.Raycast(spawns[index].transform.TransformPoint(Vector2.right * .1f), spawns[index].transform.TransformDirection(Vector2.right + (Vector2.up * .9f)), 20, room);
        rayList[6] = Physics2D.Raycast(spawns[index].transform.TransformPoint(Vector2.right * .1f), spawns[index].transform.TransformDirection(Vector2.right + (Vector2.up * -.9f)), 20, room);

        /*
        Debug.DrawRay(spawns[index].transform.position, spawns[index].transform.right * 20, Color.red, 100);
        Debug.DrawRay(spawns[index].transform.position, (spawns[index].transform.right + (spawns[index].transform.up * .3f)) * 20, Color.red, 100);
        Debug.DrawRay(spawns[index].transform.position, (spawns[index].transform.right + (spawns[index].transform.up * -.3f)) * 20, Color.red, 100);
        Debug.DrawRay(spawns[index].transform.position, (spawns[index].transform.right + (spawns[index].transform.up * .6f)) * 20, Color.red, 100);
        Debug.DrawRay(spawns[index].transform.position, (spawns[index].transform.right + (spawns[index].transform.up * -.6f)) * 20, Color.red, 100);
        Debug.DrawRay(spawns[index].transform.position, (spawns[index].transform.right + (spawns[index].transform.up * .9f)) * 20, Color.red, 100);
        Debug.DrawRay(spawns[index].transform.position, (spawns[index].transform.right + (spawns[index].transform.up * -.9f)) * 20, Color.red, 100);
        */

        if ((rayList[0] || rayList[1] || rayList[2] || rayList[3] || rayList[4]) || failed)
        {

            for (int i = 0; i < rayList.Length; i++)
            {

                if (rayList[i].distance < distance || distance == 0)
                {

                    distance = rayList[i].distance;

                }

            }

            if(distance > 0 && distance < 3)
            {

                return Instantiate(spawns[index].GetComponent<SpawnPoint>().preset.GetEnding(0).GetObj(), spawns[index].transform.position, spawns[index].transform.rotation, grid.transform);

            }
            else if (distance >= 3 && distance < 8)
            {

                return Instantiate(spawns[index].GetComponent<SpawnPoint>().preset.GetEnding(1).GetObj(), spawns[index].transform.position, spawns[index].transform.rotation, grid.transform);

            }
            else if (distance >= 8 && distance < 12)
            {

                return Instantiate(spawns[index].GetComponent<SpawnPoint>().preset.GetEnding(2).GetObj(), spawns[index].transform.position, spawns[index].transform.rotation, grid.transform);

            }
            else if (distance >= 12 && distance < 20)
            {

                return Instantiate(spawns[index].GetComponent<SpawnPoint>().preset.GetEnding(3).GetObj(), spawns[index].transform.position, spawns[index].transform.rotation, grid.transform);

            }
            else
            {

                return Instantiate(spawns[index].GetComponent<SpawnPoint>().preset.GetRandEnding().GetObj(), spawns[index].transform.position, spawns[index].transform.rotation, grid.transform);

            }


        }


        return Instantiate(chosenList.GetRoom(), spawns[index].transform.position, spawns[index].transform.rotation, grid.transform);

    }

}
