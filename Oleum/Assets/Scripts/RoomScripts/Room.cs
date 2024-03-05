using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Room : MonoBehaviour
{

    public GameObject[] spawns;

    [SerializeField] private RandList chosenList;

    [SerializeField] private MapGenerator mapGen;

    [SerializeField] private LayerMask room;


    public void SetMapGen(MapGenerator mapGen)
    {

        this.mapGen = mapGen;

    }

    public GameObject spawn(int index, Grid grid)
    {

        int spawnChance;

        int chanceThresh;

        chosenList = spawns[index].GetComponent<SpawnPoint>().preset.GetList();

        spawnChance = Random.Range(0, 100);

        chanceThresh = 95;

        if (spawnChance < chanceThresh && CheckForRooms(index))
        {

            mapGen.AddRoomAmt(chosenList.GetAmt());
            return Instantiate(chosenList.GetRoom(), spawns[index].transform.position, spawns[index].transform.rotation, grid.transform);

        }
        else
        {

            return null;

        }


    }

    public bool CheckForRooms(int num)
    {

        RaycastHit2D ray = Physics2D.Raycast(spawns[num].transform.TransformPoint(Vector2.right * .1f), spawns[num].transform.right, 20, room);
        RaycastHit2D ray1 = Physics2D.Raycast(spawns[num].transform.TransformPoint(Vector2.right * .1f), spawns[num].transform.TransformDirection(Vector2.right + (Vector2.up * .2f)), 20, room);
        RaycastHit2D ray2 = Physics2D.Raycast(spawns[num].transform.TransformPoint(Vector2.right * .1f), spawns[num].transform.TransformDirection(Vector2.right + (Vector2.up * -.2f)), 20, room);
        RaycastHit2D ray3 = Physics2D.Raycast(spawns[num].transform.TransformPoint(Vector2.right * .1f), spawns[num].transform.TransformDirection(Vector2.right + (Vector2.up * .4f)), 20, room);
        RaycastHit2D ray4 = Physics2D.Raycast(spawns[num].transform.TransformPoint(Vector2.right * .1f), spawns[num].transform.TransformDirection(Vector2.right + (Vector2.up * -.4f)), 20, room);

        Debug.DrawRay(spawns[num].transform.position, spawns[num].transform.right * 20, Color.red, 100);
        Debug.DrawRay(spawns[num].transform.position, (spawns[num].transform.right + (spawns[num].transform.up * .2f)) * 20, Color.red, 100);
        Debug.DrawRay(spawns[num].transform.position, (spawns[num].transform.right + (spawns[num].transform.up * -.2f)) * 20, Color.red, 100);
        Debug.DrawRay(spawns[num].transform.position, (spawns[num].transform.right + (spawns[num].transform.up * .4f)) * 20, Color.red, 100);
        Debug.DrawRay(spawns[num].transform.position, (spawns[num].transform.right + (spawns[num].transform.up * -.4f)) * 20, Color.red, 100);

        if ((ray || ray1 || ray2 || ray3 || ray4))
        {

            return false;

        }

        return true;

    }

}
