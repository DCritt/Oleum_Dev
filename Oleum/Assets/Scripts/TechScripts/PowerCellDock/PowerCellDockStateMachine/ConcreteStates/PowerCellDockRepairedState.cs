using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCellDockRepairedState : PowerCellDockState
{
    public PowerCellDockRepairedState(PowerCellDock dock, PowerCellDockStateMachine stateMachine) : base(dock, stateMachine)
    {



    }

    public override void InteractAction(Player player)
    {
        base.InteractAction(player);
    }

    public override void EnterState()
    {

        base.EnterState();

        dock.GetPlayer().UIManager.UpdateObjectives(1, 0);

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
