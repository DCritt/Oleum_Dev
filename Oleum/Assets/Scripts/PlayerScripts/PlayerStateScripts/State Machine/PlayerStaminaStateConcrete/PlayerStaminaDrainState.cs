using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaminaDrainState : PlayerStaminaState
{
    public PlayerStaminaDrainState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {



    }

    public override void EnterState()
    {
        base.EnterState();

        player.StaminaCounter = 0;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        DrainStamina();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void DrainStamina()
    {

        if ((player.Stamina - player.StaminaDrain * Time.deltaTime) < 0)
        {

            player.Stamina = 0;
            player.StateMachine.ChangeState(player.StaminaNotFullState);

        }
        else
        {

            player.DrainStamina();

        }

    }
}
