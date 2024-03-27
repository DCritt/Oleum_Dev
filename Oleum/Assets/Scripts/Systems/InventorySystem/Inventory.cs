using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory
{

    public Item[] Slots = new Item[5] { null, null, null, null, null };
    public int currentItem;
    private Player player;

    public InventoryStateMachine stateMachine { get; set; }

    public InventoryActiveState ActiveState { get; set; }
    public InventoryDeactiveState DeactiveState { get; set; }
    public InventoryLockedState LockedState { get; set; }

    public Inventory(Player player)
    {

        this.player = player;

    }


    public string GetHeavyItemName()
    {

        if (Slots[4] != null)
        {

            return Slots[4].data.displayName;

        }
        else
        {

            return "none";

        }

    }

    public void AddItem(NormalItemData data)
    {

        stateMachine.CurrInventoryState.AddItem(data);

    }

    public void AddItem(HeavyItemData data)
    {

        stateMachine.CurrInventoryState.AddItem(data);

    }


    public bool IsFull(NormalItemData data)
    {

        for (int i = 0; i < 4; i++)
        {

            if (Slots[i] == null)
            {

                return false;

            }

        }

        return true;

    }

    public bool IsFull(HeavyItemData data)
    {

        if (Slots[4] == null)
        {

            return false;

        }

        return true;

    }

    public void RemoveCurrentItem()
    {

        stateMachine.CurrInventoryState.RemoveCurrentItem();

    }

    public void DeleteCurrentItem()
    {

        stateMachine.CurrInventoryState.DeleteCurrentItem();

    }

    public void SetCurrentItem(int item)
    {

        stateMachine.CurrInventoryState.SetCurrentItem(item);

    }

    public void UseCurrentItem()
    {

        stateMachine.CurrInventoryState.UseCurrentItem();

    }

}
