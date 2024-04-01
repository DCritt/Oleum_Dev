using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCellDockDeadState : PowerCellDockState
{
    public PowerCellDockDeadState(PowerCellDock dock, PowerCellDockStateMachine stateMachine) : base(dock, stateMachine)
    {



    }

    public override void InteractAction()
    {

        if (dock.GetPlayer().Inventory.IsFull(GameManagerScript.instance.GetItemDataType("HeavyUtility") as HeavyItemData))
        {



        }
        else
        {

            dock.GetPowerCellDockAnimator().SetInteger("CurrAnim", 0);
            dock.stateMachine.ChangeState(dock.emptyState);
            dock.GetPlayer().Inventory.AddItem(GameManagerScript.instance.GetItemData("PowerCell(Deactive)") as HeavyUtilityItemData, null);

        }

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

}
