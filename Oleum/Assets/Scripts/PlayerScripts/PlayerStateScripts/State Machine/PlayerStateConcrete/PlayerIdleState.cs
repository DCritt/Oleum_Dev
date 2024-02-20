using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {



    }

    public override void EnterState()
    {

        base.EnterState();

        player.SetAnimation(0);

    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {

        base.FrameUpdate();

        player.FollowMouse();

        TestWalking();
        TestSprinting();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void TestWalking()
    {

        if (player.IsWalking())
        {

            player.StateMachine.ChangeState(player.WalkingState);

        }

    }

    public void TestSprinting()
    {

        if (player.IsSprinting())
        {

            player.StateMachine.ChangeState(player.SprintState);

        }

    }

}
