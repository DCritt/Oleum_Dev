using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Room : MonoBehaviour
{

    public GameObject[] spawns;

    public GameObject[] rooms;
    public GameObject[] halls;

    public int roomChance;
    public int hallChance;

    public void SetLists(GameObject[] rooms, GameObject[] halls)
    {

        this.rooms = rooms;
        this.halls = halls;

    }

    public GameObject spawn(int index, int room, GameObject[] rooms, Grid grid)
    {

        int chance;
        int chanceThresh;

        chance = Random.Range(0, 20);

        if (rooms == this.rooms)
        {

            chanceThresh = roomChance;

        }
        else if (rooms == this.halls)
        {

            chanceThresh = hallChance;

        }
        else
        {

            chanceThresh = 20;

        }

        if (chance > chanceThresh)
        {

            return Instantiate(rooms[room], spawns[index].transform.position, spawns[index].transform.rotation, grid.transform);
        
        }
        else
        {

            return null;

        }
       

    }

}
