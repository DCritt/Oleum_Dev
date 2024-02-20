using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : IInteractable
{
    
    public ItemData data { get; private set; }
    public Player player { get; private set; }
    public GameObject inHandGameObject;
    public ItemInteractScript interactScript;
    public GameObject droppedItem;
    public event Action onLeftClick;
    public event Action<float> onItemGone;

    public float multi;

    public Item(NormalItemData data, Player player)
    {

        this.data = data;
        this.player = player;

        inHandGameObject = UnityEngine.Object.Instantiate(data.inHandGameObjectPrefab, player.transform.GetChild(4).position, player.transform.GetChild(4).rotation, player.transform.GetChild(4));
        interactScript = inHandGameObject.GetComponent<ItemInteractScript>();

        //onLeftClick += player.Inventory.DeleteCurrentItem;
        onLeftClick += interactScript.Interact;

    }

    public Item(HeavyItemData data, Player player)
    {

        this.data = data;
        this.player = player;
        multi = data.slowMultiplier;

        player.SetSpeedMultiplier(data.slowMultiplier);

        inHandGameObject = UnityEngine.Object.Instantiate(data.inHandGameObjectPrefab, player.transform.GetChild(5).position, player.transform.GetChild(5).rotation, player.transform.GetChild(5));
        interactScript = inHandGameObject.GetComponent<ItemInteractScript>();

        onLeftClick += interactScript.Interact;
        onItemGone += player.UnsetSpeedMultiplier;

    }

    public void Interact()
    {
      
        onLeftClick.Invoke();

    }

    public void SelectItem()
    {

        inHandGameObject.SetActive(true);

    }

    public void DeselectItem()
    {

        inHandGameObject.SetActive(false);

    }

    public void RemoveItem()
    {

        onLeftClick = null;
        UnityEngine.Object.Destroy(inHandGameObject);
        droppedItem = UnityEngine.Object.Instantiate(data.pickupPrefab, new Vector3(player.transform.position.x, player.transform.position.y, (player.transform.position.z + (float).1)), player.transform.rotation);
        droppedItem.GetComponent<ItemPickup>().SetData(data);
        droppedItem = null;
        onItemGone?.Invoke(multi);

    }

    public void DeleteItem()
    {

        onLeftClick = null;
        UnityEngine.Object.Destroy(inHandGameObject);
        onItemGone?.Invoke(multi);

    }

}
