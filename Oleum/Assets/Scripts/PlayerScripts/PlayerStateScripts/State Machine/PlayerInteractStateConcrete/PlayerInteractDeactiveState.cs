using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractDeactiveState : PlayerInteractState
{
    public PlayerInteractDeactiveState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {



    }

    public override void EnterState()
    {
        base.EnterState();

        UIManagerScript.Instance.SwitchInteractDeactive();

    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
