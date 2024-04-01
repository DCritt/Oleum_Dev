using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDeactiveState : InventoryState
{
    public InventoryDeactiveState(Inventory inventory, Player player, InventoryStateMachine stateMachine) : base(inventory, player, stateMachine)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void AddItem(NormalItemData data, GameObject obj)
    {
        base.AddItem(data, obj);
    }

    public override void AddItem(HeavyItemData data, GameObject obj)
    {
        base.AddItem(data, obj);
    }

    public override void RemoveCurrentItem()
    {
        base.RemoveCurrentItem();
    }

    public override void DeleteCurrentItem()
    {
        base.DeleteCurrentItem();
    }

    public override void SetCurrentItem(int item)
    {
        base.SetCurrentItem(item);
    }

    public override void UseCurrentItem()
    {
        base.UseCurrentItem();
    }
}
