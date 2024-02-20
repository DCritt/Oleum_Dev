using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthDrainState : PlayerHealthState
{
    public PlayerHealthDrainState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
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

        DrainHealth();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void DrainHealth()
    {

        if ((player.Health - player.HealthDrain * Time.deltaTime) < 0)
        {

            player.Health = 0;
            player.Die();

        }
        else
        {

            player.DrainHealth();

        }

    }
}
