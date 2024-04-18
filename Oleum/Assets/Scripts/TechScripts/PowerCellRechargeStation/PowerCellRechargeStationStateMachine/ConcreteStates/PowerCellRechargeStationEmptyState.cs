using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCellRechargeStationEmptyState : PowerCellRechargeStationState
{
    public PowerCellRechargeStationEmptyState(PowerCellRechargeStation rechargeStation, PowerCellRechargeStationStateMachine stateMachine) : base(rechargeStation, stateMachine)
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

    public override void InteractAction(Player player)
    {

        if (player.Inventory.GetHeavyItemName() == "PowerCell(Deactive)")
        {

            player.Inventory.DeleteCurrentItem();
            rechargeStation.stateMachine.ChangeState(rechargeStation.fullState);

        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override string GetInteractText()
    {

        return "Recharge PowerCell";

    }

}
