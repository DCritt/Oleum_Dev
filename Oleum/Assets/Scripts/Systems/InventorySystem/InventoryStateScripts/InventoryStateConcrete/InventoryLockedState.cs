using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryLockedState : InventoryState
{
    public InventoryLockedState(Inventory inventory, Player player, InventoryStateMachine stateMachine) : base(inventory, player, stateMachine)
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

    public override void AddItem(NormalItemData data, GameObject obj)
    {

        base.AddItem(data, obj);

        for (int i = 0; i < 4; i++)
        {

            if (inventory.Slots[i] == null)
            {

                inventory.Slots[i] = new NormalItem(data as NormalItemData, player, obj);
                player.UIManager.SetImage(i, inventory.Slots[i]);
                inventory.Slots[i]?.DeselectItem();
                return;

            }

        }

    }

    public override void AddItem(HeavyItemData data, GameObject obj)
    {

        base.AddItem(data, obj);

    }

    public override void RemoveCurrentItem()
    {

        base.RemoveCurrentItem();

        inventory.Slots[inventory.currentItem]?.RemoveItem();
        inventory.Slots[inventory.currentItem] = null;
        player.PlayerBodyAnim.SetInteger("CurrAnim", 0);
        inventory.stateMachine.ChangeState(inventory.ActiveState);

    }

    public override void DeleteCurrentItem()
    {

        base.DeleteCurrentItem();

        inventory.Slots[inventory.currentItem]?.DeleteItem();
        inventory.Slots[inventory.currentItem] = null;
        player.PlayerBodyAnim.SetInteger("CurrAnim", 0);
        inventory.stateMachine.ChangeState(inventory.ActiveState);

    }

    public override void SetCurrentItem(int item)
    {

        base.SetCurrentItem(item);

        player.UIManager.SetSelect(inventory.currentItem, item);
        inventory.Slots[inventory.currentItem]?.DeselectItem();
        inventory.currentItem = item;
        inventory.Slots[inventory.currentItem]?.SelectItem();

    }

    public override void UseCurrentItem()
    {
        base.UseCurrentItem();
    }

    public void TestInputs()
    {

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
