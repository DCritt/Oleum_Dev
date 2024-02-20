using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightScript : ItemInteractScript
{

    [SerializeField] private GameObject flashlight;

    public override void Interact()
    {

        base.Interact();

        flashlight.SetActive(!(flashlight.activeInHierarchy));

    }
}
