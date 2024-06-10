using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Objective : ScriptableObject
{
    
    public string objectiveName;
    public string description;
    [SerializeField] private int maxProgress;
    public GameObject objectiveObj;
    public List<GameObject> mainRooms;
    public List<GameObject> sideRooms;


    public int GetMaxProgress()
    {

        return maxProgress;

    }

}
