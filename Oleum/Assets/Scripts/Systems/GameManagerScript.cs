using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    public static GameManagerScript instance;

    public Objective mainObjective;
    public Objective sideObjective;

    [SerializeField] private MapGenerator generator;

    [SerializeField] private Grid grid;
    [SerializeField] private GameObject spawnRoom;
    [SerializeField] private GameObject[] rooms;
    [SerializeField] private GameObject[] halls;

    private Count count;

    [SerializeField] private ItemData[] itemDataList = new ItemData[0];
    [SerializeField] private ItemData[] itemDataTypeList = new ItemData[0];

    public ItemData GetItemData(string name)
    {

        for (int i = 0; i < itemDataList.Length; i++)
        {

            if (itemDataList[i].displayName == name)
            {

                return itemDataList[i];

            }

        }

        return null;

    }

    public ItemData GetItemDataType(string name)
    {

        for (int i = 0; i < itemDataTypeList.Length; i++)
        {

            if (itemDataTypeList[i].displayName == name)
            {

                return itemDataTypeList[i];

            }

        }

        return null;

    }

    private void Awake()
    {

        instance = this;
        count = new Count(6, 10);

    }

    private void Start()
    {

        generator.SetGenerator(grid, spawnRoom, rooms, halls, count);

    }

}
