using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthHealingState : PlayerHealthState
{
    public PlayerHealthHealingState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
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

        GainHealth();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void GainHealth()
    {

        if ((player.Health + player.HealthGain * Time.deltaTime) > player.MaxHealth)
        {

            player.Health = player.MaxHealth;
            player.StateMachine.ChangeState(player.HealthFullState);

        }
        else
        {

            player.GainHealth();

        }

    }
}
