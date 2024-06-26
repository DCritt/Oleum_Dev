using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyItem : Item
{

    protected HeavyItemData heavyData;

    public HeavyItem(HeavyItemData data, Player player, GameObject obj) : base(data, player)
    {

        heavyData = data;

        player.SetSpeedMultiplier(data.slowMultiplier);

        if (obj == null)
        {

            inHandGameObject = UnityEngine.Object.Instantiate(data.inHandGameObjectPrefab, player.heavyHolder.transform.position, player.heavyHolder.transform.rotation, player.heavyHolder.transform);

        }
        else
        {

            inHandGameObject = obj;
            inHandGameObject.transform.SetParent(player.heavyHolder.transform);
            inHandGameObject.transform.SetPositionAndRotation(player.heavyHolder.transform.position, player.heavyHolder.transform.rotation);

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
