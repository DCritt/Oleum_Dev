using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyItem : Item
{

    protected HeavyItemData heavyData;

    public HeavyItem(HeavyItemData data, Player player) : base(data, player)
    {

        heavyData = data;

        player.SetSpeedMultiplier(data.slowMultiplier);

        Debug.Log("worked");

        if (data.holder == null)
        {

            Debug.Log("null");

            inHandGameObject = UnityEngine.Object.Instantiate(data.inHandGameObjectPrefab, player.transform.GetChild(5).position, player.transform.GetChild(5).rotation, player.transform.GetChild(5));
            data.holder = inHandGameObject;

        }
        else
        {

            Debug.Log("exists");

            inHandGameObject = data.holder;
            inHandGameObject.transform.SetParent(player.transform.GetChild(5));
            inHandGameObject.transform.SetPositionAndRotation(player.transform.GetChild(5).position, player.transform.GetChild(5).rotation);

        }

        interactScript = inHandGameObject.GetComponent<ItemInteractScript>();


    }

    public override void DeleteItem()
    {

        base.DeleteItem();

        player.UnsetSpeedMultiplier(heavyData.slowMultiplier);

    }

    public override void DeselectItem()
    {

        base.DeselectItem();

    }

    public override void Interact()
    {

        base.Interact();

    }

    public override void RemoveItem()
    {

        base.RemoveItem();

        player.UnsetSpeedMultiplier(heavyData.slowMultiplier);

    }

    public override void SelectItem()
    {

        base.SelectItem();


    }

}
