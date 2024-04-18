using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalItem : Item
{

    protected NormalItemData normalData;

    public NormalItem(NormalItemData data, Player player, GameObject obj) : base(data, player)
    {

        normalData = data;

        if (obj == null)
        {

            Debug.Log("null");

            inHandGameObject = UnityEngine.Object.Instantiate(data.inHandGameObjectPrefab, player.normalHolder.transform.position, player.normalHolder.transform.rotation, player.normalHolder.transform);

        }
        else
        {

            Debug.Log("exists");
            inHandGameObject = obj;
            inHandGameObject.transform.SetParent(player.normalHolder.transform);
            inHandGameObject.transform.SetPositionAndRotation(player.normalHolder.transform.position, player.normalHolder.transform.rotation);

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
