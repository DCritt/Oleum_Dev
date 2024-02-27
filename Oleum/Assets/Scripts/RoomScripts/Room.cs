using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public GameObject[] spawns;

    public GameObject spawn(int index, int room, GameObject[] rooms, Grid grid)
    {

        return Instantiate(rooms[room], spawns[index].transform.position, spawns[index].transform.rotation, grid.transform);
        
    }

}
