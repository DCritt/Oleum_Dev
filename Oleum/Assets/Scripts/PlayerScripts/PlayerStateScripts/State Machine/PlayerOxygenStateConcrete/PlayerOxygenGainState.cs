using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOxygenGainState : PlayerOxygenState
{
    public PlayerOxygenGainState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
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

        GainOxygen();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void GainOxygen()
    {

        if ((player.Oxygen + player.OxygenGain * Time.deltaTime) > player.MaxOxygen)
        {

            player.SetOxygen(player.GetMaxOxygen());
            player.StateMachine.ChangeState(player.OxygenFullState);

        }
        else
        {

            player.GainOxygen();

        }

    }
}
