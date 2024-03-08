using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class RandList
{

    [SerializeField] private RandObject[] roomList;

    [Range(0, 100)]
    [SerializeField] private float chance;
    [SerializeField] private int amt;

    public int GetAmt()
    {

        return amt;

    }

    public float GetChance()
    {

        return chance;

    }

    public GameObject GetRoom()
    {

        float randNum;
        float currentChance = 0;

        randNum = Random.Range(0, 100);

        for (int i = 0; i < roomList.Length; i++)
        {

            currentChance += roomList[i].GetChance();

            if (randNum < currentChance)
            {

                return roomList[i].GetObj();

            }

        }

        return null;

    }

}

[Serializable]
public class RandObject
{

    [SerializeField] private GameObject obj;

    [Range(0, 100)]
    [SerializeField] private float chance;

    public float GetChance()
    {

        return chance;

    }

    public GameObject GetObj()
    {

        return obj;

    }

}

    

