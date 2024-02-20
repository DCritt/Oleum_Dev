using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOxygenDrainState : PlayerOxygenState
{
    public PlayerOxygenDrainState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
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

        DrainOxygen();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void DrainOxygen()
    {

        if ((player.Oxygen - player.OxygenDrain * Time.deltaTime) < 0)
        {

            player.SetOxygen(0);
            player.StateMachine.ChangeState(player.OxygenEmptyState);

        }
        else
        {

            player.DrainOxygen();

        }

    }
}
