using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightScript : ItemInteractScript
{

    [SerializeField] private GameObject flashlight;

    public override void Interact(Player player)
    {

        base.Interact(player);

        flashlight.SetActive(!(flashlight.activeInHierarchy));

    }
}
