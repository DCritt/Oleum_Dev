using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaminaGainState : PlayerStaminaState
{
    public PlayerStaminaGainState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
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

        GainStamina();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void GainStamina()
    {

        if ((player.Stamina + player.StaminaGain * Time.deltaTime) > player.MaxStamina)
        {

            player.Stamina = player.MaxStamina;
            player.StateMachine.ChangeState(player.StaminaFullState);

        }
        else
        {

            player.GainStamina();

        }

    }
}
