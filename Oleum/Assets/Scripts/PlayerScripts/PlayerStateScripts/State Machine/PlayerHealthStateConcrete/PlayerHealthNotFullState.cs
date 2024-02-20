using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthNotFullState : PlayerHealthState
{
    public PlayerHealthNotFullState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
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

        TestTimer();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void TestTimer()
    {

        if (player.TestHealthCounter())
        {

            player.StateMachine.ChangeState(player.HealthHealingState);

        }
        else
        {

            player.CountHealthDelay();

        }

    }
}
