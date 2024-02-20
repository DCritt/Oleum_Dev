using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractActiveState : PlayerInteractState
{

    public delegate void Interact();
    private Interact interact;

    public PlayerInteractActiveState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {



    }

    public override void EnterState()
    {

        base.EnterState();

        player.UIManager.SwitchInteractActive();

    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        TestInteract();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetInteract(Interact interact)
    {

        this.interact = interact;

    }

    public void TestInteract()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {

            interact?.Invoke();

        }

    }
}
