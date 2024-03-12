using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : ScriptableObject
{

    public string name;
    public string description;
    [SerializeField] private int maxProgress;
    private int currProgress;
    public GameObject objectiveObj;
    public ObjectiveRooms rooms;

    [Serializable]
    public class ObjectiveRooms
    {

        [SerializeField] private GameObject mainRoom;
        [SerializeField] private GameObject[] sideRooms;

        public GameObject GetMainRoom()
        {

            return mainRoom;
            
        }

        public GameObject[] GetSideRooms(int index)
        {

            return sideRooms;

        }

    }

    public void Progress(int amt)
    {

        currProgress += amt;

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

}
