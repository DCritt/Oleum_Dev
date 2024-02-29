using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPoint : MonoBehaviour
{

    [Serializable]
    public class RandLists
    {

        [SerializeField] private GameObject[] roomList;

        [Range(0, 100)]
        [SerializeField] private float chance;

        public float GetChance()
        {

            return chance;

        }

        public GameObject[] GetRoomsList()
        {

            return roomList;

        }

    }

    [SerializeField] private RandLists[] holder;

    public GameObject[] GetList()
    {

        float randNum;
        float currentChance = 0;

        randNum = Random.Range(0, 100);

        for (int i = 0; i < holder.Length; i++)
        {

            currentChance += holder[i].GetChance();

            if (randNum < currentChance)
            {

                return holder[i].GetRoomsList();

            }

        }

        return null;

    }

}
