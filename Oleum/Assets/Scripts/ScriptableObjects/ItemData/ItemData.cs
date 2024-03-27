using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Inventory Item Data")]
public class ItemData : ScriptableObject
{

    public string id;
    public string displayName;
    public string description;
    protected int type;
    public Sprite icon;
    public GameObject pickupPrefab;
    public GameObject inHandGameObjectPrefab;
    [HideInInspector] public GameObject holder = null;
    
    public int getType()
    {

        return type;

    }

    public virtual void SetType()
    {

        

    }

}
