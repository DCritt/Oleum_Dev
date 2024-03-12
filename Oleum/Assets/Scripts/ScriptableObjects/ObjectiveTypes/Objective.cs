using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : ScriptableObject
{

    public string name;
    public string description;
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
        
}
