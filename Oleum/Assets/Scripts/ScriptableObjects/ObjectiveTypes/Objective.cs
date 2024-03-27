using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Objective : ScriptableObject
{
    [Serializable]
    public class ObjectiveRooms
    {

        [SerializeField] private RandObject[] mainRooms;
        [SerializeField] private RandObject[] sideRooms;

        public GameObject GetRandomMainRoom()
        {

            float randNum;
            float currentChance = 0;

            randNum = Random.Range(0, 100);

            for (int i = 0; i < mainRooms.Length; i++)
            {

                currentChance += mainRooms[i].GetChance();

                if (randNum < currentChance)
                {

                    return mainRooms[i].GetObj();

                }

            }

            return null;

        }

        public GameObject GetMainRoom(int index)
        {

            return mainRooms[index].GetObj();

        }

        public GameObject GetRandomSideRooms()
        {

            float randNum;
            float currentChance = 0;

            randNum = Random.Range(0, 100);

            for (int i = 0; i < sideRooms.Length; i++)
            {

                currentChance += sideRooms[i].GetChance();

                if (randNum < currentChance)
                {

                    return sideRooms[i].GetObj();

                }

            }

            return null;

        }

        public GameObject GetSideRoom(int index)
        {

            return sideRooms[index].GetObj();

        }

    }

    public string objectiveName;
    public string description;
    [SerializeField] private int maxProgress;
    private int currProgress;
    public GameObject objectiveObj;
    public ObjectiveRooms rooms;


    public void Progress(int amt)
    {

        currProgress += amt;
        UIManagerScript.UpdateObjectives();

    }

    public int GetProgress()
    {

        return currProgress;

    }

    public int GetMaxProgress()
    {

        return maxProgress;

    }

    public bool IsComplete()
    {

        if (currProgress < maxProgress)
        {

            return false;

        }
        else
        {

            return true;

        }

    }

    public void ResetObjective()
    {

        currProgress = 0;

    }

}
