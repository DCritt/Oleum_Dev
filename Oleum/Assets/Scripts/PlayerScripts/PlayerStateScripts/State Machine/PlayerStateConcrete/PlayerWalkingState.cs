using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkingState : PlayerState
{

    private Vector2 _direction;

    public PlayerWalkingState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {



    }

    public override void EnterState()
    {

        base.EnterState();

        player.SetAnimation(1);

    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        player.FollowMouse();

        GetDirections();

        TestIdle();
        TestSprinting();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        player.MovePlayer(_direction);

    }

    public void TestIdle()
    {

        if (player.IsIdle())
        {

            player.MovePlayer(new Vector2(0, 0));
            player.StateMachine.ChangeState(player.IdleState);

        }

    }

    public void TestSprinting()
    {

        if (player.IsSprinting())
        {

            player.StateMachine.ChangeState(player.SprintState);

        }

    }

    public void GetDirections()
    {

        _direction = (new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * player.MoveSpeed);


    }

}
