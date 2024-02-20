using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaminaNotFullState : PlayerStaminaState
{
    public PlayerStaminaNotFullState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
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

        if (player.TestStaminaCounter())
        {

            player.StateMachine.ChangeState(player.StaminaGainState);

        }
        else
        {

            player.CountStaminaDelay();

        }

    }
}
