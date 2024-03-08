using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpawnPointPreset")]
public class SpawnPointPreset : ScriptableObject
{

    [SerializeField] private RandList[] holder;
    [SerializeField] private RandObject[] endings;

    public RandObject GetEnding(int index)
    {

        if (index < endings.Length)
        {

            return endings[index];

        }
        else
        {

            return null;

        }

    }

    public RandObject GetRandEnding()
    {

        float randNum;
        float currentChance = 0;

        randNum = Random.Range(0, 100);

        for (int i = 0; i < endings.Length; i++)
        {

            currentChance += endings[i].GetChance();

            if (randNum < currentChance)
            {

                return endings[i];

            }

        }

        return null;

    }

    public RandList GetList(int index)
    {

        if (index < holder.Length)
        {

            return holder[index];

        }
        else
        {

            return null;

        }

    }

    public RandList GetRandList()
    {

        float randNum;
        float currentChance = 0;

        randNum = Random.Range(0, 100);

        for (int i = 0; i < holder.Length; i++)
        {

            currentChance += holder[i].GetChance();

            if (randNum < currentChance)
            {

                return holder[i];

            }

        }

        return null;

    }

}
