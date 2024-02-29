using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpawnPointPreset")]
public class SpawnPointPreset : ScriptableObject
{

    public RandList[] holder;

    public RandList GetList()
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
