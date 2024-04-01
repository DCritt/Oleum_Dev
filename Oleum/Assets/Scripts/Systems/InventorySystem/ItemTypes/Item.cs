using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : IInteractable
{

    public ItemData data;
    protected Player player; 
    protected GameObject inHandGameObject;
    protected ItemInteractScript interactScript;
    protected GameObject droppedItem;

    public Item(ItemData data, Player player)
    {

        this.data = data;
        this.player = player;

    }

    public virtual void Interact()
    {

        interactScript.Interact();

    }

    public virtual void SelectItem()
    {

        inHandGameObject.SetActive(true);

    }

    public virtual void DeselectItem()
    {

        inHandGameObject.SetActive(false);

    }

    public virtual void RemoveItem()
    {

        //UnityEngine.Object.Destroy(inHandGameObject);
        droppedItem = UnityEngine.Object.Instantiate(data.pickupPrefab, new Vector3(player.transform.position.x, player.transform.position.y, (player.transform.position.z + (float).1)), player.transform.rotation);
        droppedItem.GetComponent<ItemPickup>().SetData(data, inHandGameObject);
        droppedItem = null;
        //inHandGameObject = null;

    }

    public virtual void DeleteItem()
    {

        UnityEngine.Object.Destroy(inHandGameObject);

    }

}
