using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalItem : Item
{

    protected NormalItemData normalData;

    public NormalItem(NormalItemData data, Player player, GameObject obj) : base(data, player, obj)
    {

        normalData = data;

        inHandGameObject.transform.SetParent(player.transform.GetChild(4));
        inHandGameObject.transform.position = player.transform.GetChild(4).position;
        inHandGameObject.transform.rotation = player.transform.GetChild(4).rotation;
        //inHandGameObject = UnityEngine.Object.Instantiate(data.inHandGameObjectPrefab, player.transform.GetChild(4).position, player.transform.GetChild(4).rotation, player.transform.GetChild(4));
        interactScript = inHandGameObject.GetComponent<ItemInteractScript>();


    }

    public override void DeleteItem()
    {
        base.DeleteItem();
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
    }

    public override void SelectItem()
    {
        base.SelectItem();
    }
}
