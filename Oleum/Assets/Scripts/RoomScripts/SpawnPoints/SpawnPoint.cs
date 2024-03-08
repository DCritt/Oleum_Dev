using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPoint : MonoBehaviour
{

    private bool active = true;
    public SpawnPointPreset preset;

    public void Deactivate()
    {

        active = false;

    }

    public bool GetState()
    {

        return active;

    }

}
