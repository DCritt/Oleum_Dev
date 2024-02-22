using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryActiveState : InventoryState
{
    public InventoryActiveState(Inventory inventory, Player player, InventoryStateMachine stateMachine) : base(inventory, player, stateMachine)
    {



    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        TestInputs();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void AddItem(NormalItemData data)
    {
        base.AddItem(data);

        for (int i = 0; i < 4; i++)
        {

            if (inventory.Slots[i] == null)
            {

                inventory.Slots[i] = new NormalItem(data as NormalItemData, player);
                player.UIManager.SetImage(i, inventory.Slots[i]);
                SetCurrentItem(i);
                player.UIManager.SetSelect(i);
                return;

            }

        }

    }

    public override void AddItem(HeavyItemData data)
    {

        base.AddItem(data);

    }

    public override void RemoveCurrentItem()
    {

        base.RemoveCurrentItem();

        inventory.Slots[inventory.currentItem]?.RemoveItem();
        inventory.Slots[inventory.currentItem] = null;
        player.UIManager.SetImage(inventory.currentItem, inventory.Slots[inventory.currentItem]);
        player.PlayerBodyAnim.SetInteger("CurrAnim", 0);

    }

    public override void DeleteCurrentItem()
    {

        base.DeleteCurrentItem();

        inventory.Slots[inventory.currentItem]?.DeleteItem();
        inventory.Slots[inventory.currentItem] = null;
        player.UIManager.SetImage(inventory.currentItem, inventory.Slots[inventory.currentItem]);
        player.PlayerBodyAnim.SetInteger("CurrAnim", 0);

    }

    public override void SetCurrentItem(int item)
    {

        base.SetCurrentItem(item);

        player.UIManager.SetSelect(inventory.currentItem, item);
        inventory.Slots[inventory.currentItem]?.DeselectItem();
        inventory.currentItem = item;
        inventory.Slots[inventory.currentItem]?.SelectItem();
        if (inventory.Slots[inventory.currentItem] != null)
        {

            player.PlayerBodyAnim.SetInteger("CurrAnim", 1);

        }
        else
        {

            player.PlayerBodyAnim.SetInteger("CurrAnim", 0);

        }

    }

    public override void UseCurrentItem()
    {
        base.UseCurrentItem();
    }

    public void TestInputs()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            inventory.SetCurrentItem(0);

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            inventory.SetCurrentItem(1);

        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {

            inventory.SetCurrentItem(2);

        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {

            inventory.SetCurrentItem(3);

        }

        if (Input.GetMouseButtonDown(0))
        {

            inventory.UseCurrentItem();

        }

        if (Input.GetKeyDown(KeyCode.Q))
        {

            inventory.RemoveCurrentItem();

        }

    }

}
