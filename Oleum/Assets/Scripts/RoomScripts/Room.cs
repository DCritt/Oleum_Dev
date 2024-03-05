using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Room : MonoBehaviour
{

    public GameObject[] spawns;

    public GameObject chosenRoom;

    [SerializeField] private LayerMask room;

    public GameObject[] rooms;
    public GameObject[] halls;


    public void SetLists(GameObject[] rooms, GameObject[] halls)
    {

        this.rooms = rooms;
        this.halls = halls;

    }

    public GameObject spawn(int index, Grid grid)
    {

        int spawnChance;

        int chanceThresh;

        chosenRoom = spawns[index].GetComponent<SpawnPoint>().preset.GetList().GetRoom();

        spawnChance = Random.Range(0, 100);

        chanceThresh = 95;

        if (spawnChance < chanceThresh && CheckForRooms(index))
        {

            return Instantiate(chosenRoom, spawns[index].transform.position, spawns[index].transform.rotation, grid.transform);

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
