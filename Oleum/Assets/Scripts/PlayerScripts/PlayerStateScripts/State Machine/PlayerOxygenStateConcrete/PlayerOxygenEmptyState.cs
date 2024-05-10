using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOxygenEmptyState : PlayerOxygenState
{
    public PlayerOxygenEmptyState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {



    }

    public override void EnterState()
    {

        base.EnterState();

        player.StateMachine.ChangeState(player.HealthDrainState);

    }

    public override void ExitState()
    {

        base.ExitState();

        if (player.StateMachine.CurrPlayerHealthState != player.HealthDeadState)
        {

            player.StateMachine.ChangeState(player.HealthNotFullState);

        }

    }

    public override void FrameUpdate()
    {

        base.FrameUpdate();

        if (player.StateMachine.CurrPlayerHealthState != player.HealthDrainState) {


            player.StateMachine.ChangeState(player.HealthDrainState);

        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
