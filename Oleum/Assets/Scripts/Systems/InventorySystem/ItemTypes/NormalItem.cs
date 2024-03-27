using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalItem : Item
{

    protected NormalItemData normalData;

    public NormalItem(NormalItemData data, Player player) : base(data, player)
    {

        normalData = data;

        if (data.holder == null)
        {

            Debug.Log("null");

            inHandGameObject = UnityEngine.Object.Instantiate(data.inHandGameObjectPrefab, player.transform.GetChild(4).position, player.transform.GetChild(4).rotation, player.transform.GetChild(4));

        }
        else
        {

            Debug.Log("exists");
            inHandGameObject = data.holder;
            inHandGameObject.transform.SetParent(player.transform.GetChild(4));
            inHandGameObject.transform.SetPositionAndRotation(player.transform.GetChild(4).position, player.transform.GetChild(4).rotation);

        }

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
