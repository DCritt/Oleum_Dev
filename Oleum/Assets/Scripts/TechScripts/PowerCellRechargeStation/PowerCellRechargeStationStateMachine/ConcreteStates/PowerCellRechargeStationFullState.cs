using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCellRechargeStationFullState : PowerCellRechargeStationState
{
    public PowerCellRechargeStationFullState(PowerCellRechargeStation rechargeStation, PowerCellRechargeStationStateMachine stateMachine) : base(rechargeStation, stateMachine)
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
        
        if (!(player.Inventory.IsFull(GameManagerScript.instance.GetItemDataType("HeavyUtility") as HeavyItemData)))
        {

            player.Inventory.AddItem(GameManagerScript.instance.GetItemData("PowerCell(Active)") as HeavyUtilityItemData, null);
            rechargeStation.stateMachine.ChangeState(rechargeStation.emptyState);

        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override string GetInteractText()
    {

        return "Take PowerCell(Active)";

    }

}
