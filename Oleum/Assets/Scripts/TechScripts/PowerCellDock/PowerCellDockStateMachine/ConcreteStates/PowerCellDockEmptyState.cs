using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCellDockEmptyState : PowerCellDockState
{
    public PowerCellDockEmptyState(PowerCellDock dock, PowerCellDockStateMachine stateMachine) : base(dock, stateMachine)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override IEnumerator Dock()
    {

        dock.GetPlayer().Inventory.DeleteCurrentItem();
        dock.gameObject.GetComponent<BoxCollider2D>().enabled = false;

        dock.GetPowerCellDockAnimator().SetInteger("CurrAnim", 1);

        yield return new WaitForSeconds(dock.GetDockingClip().length / dock.GetPowerCellDockAnimator().GetCurrentAnimatorStateInfo(0).speed);

        dock.GetPowerCellDockAnimator().SetInteger("CurrAnim", 2);
        dock.stateMachine.ChangeState(dock.repairedState);

    }
}
