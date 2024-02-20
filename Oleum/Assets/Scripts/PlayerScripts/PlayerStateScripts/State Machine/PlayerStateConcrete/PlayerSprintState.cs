using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintState : PlayerState
{

    private Vector2 _direction;

    public PlayerSprintState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {



    }

    public override void EnterState()
    {

        base.EnterState();

        player.StateMachine.ChangeState(player.StaminaDrainState);
        player.SetAnimation(2);

    }

    public override void ExitState()
    {

        base.ExitState();

        player.StateMachine.ChangeState(player.StaminaNotFullState);

    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        player.FollowMouse();

        TestStamina();
        TestIdle();
        TestWalking();

        GetDirections();
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

    public void TestWalking()
    {

        if (player.IsWalking())
        {

            player.StateMachine.ChangeState(player.WalkingState);

        }

    }

    public void GetDirections()
    {

        _direction = (new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * player.MoveSpeed * player.SprintMulti);

    }

    public void TestStamina()
    {

        if (!(player.HaveStamina()))
        {

            player.StateMachine.ChangeState(player.WalkingState);
            player.StaminaCounter = 0;

        }

    }

}
