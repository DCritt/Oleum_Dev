using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCellScript : ItemInteractScript
{

    [SerializeField] private GameObject spotLight;

    public void ToggleLight()
    {

        spotLight.SetActive(!(spotLight.activeInHierarchy));

    }

    public override void Interact()
    {

        base.Interact();


    }
}
