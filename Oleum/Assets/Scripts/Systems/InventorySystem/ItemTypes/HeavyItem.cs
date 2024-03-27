using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyItem : Item
{

    protected HeavyItemData heavyData;

    public HeavyItem(HeavyItemData data, Player player, GameObject obj) : base(data, player, obj)
    {

        heavyData = data;

        player.SetSpeedMultiplier(data.slowMultiplier);

        inHandGameObject.transform.SetParent(player.transform.GetChild(5));
        inHandGameObject.transform.SetPositionAndRotation(player.transform.GetChild(5).position, player.transform.GetChild(5).rotation);
        Debug.Log("worked");
        //inHandGameObject = UnityEngine.Object.Instantiate(data.inHandGameObjectPrefab, player.transform.GetChild(5).position, player.transform.GetChild(5).rotation, player.transform.GetChild(5));
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
