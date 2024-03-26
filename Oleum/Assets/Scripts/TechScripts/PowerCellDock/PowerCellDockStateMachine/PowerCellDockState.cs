using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCellDockState
{

    protected PowerCellDock dock;
    protected PowerCellDockStateMachine stateMachine;
    protected Player player;

    public PowerCellDockState(PowerCellDock dock, PowerCellDockStateMachine stateMachine)
    {

        this.dock = dock;
        this.stateMachine = stateMachine;

    }

    public virtual void EnterState()
    {



    }

    public virtual void ExitState()
    {



    }

    public virtual void FrameUpdate()
    {



    }

    public virtual void PhysicsUpdate()
    {



    }

    public virtual IEnumerator Dock()
    {

        return null;

    }

}
