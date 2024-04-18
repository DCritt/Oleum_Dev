using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCellRechargeStationState
{

    protected PowerCellRechargeStation rechargeStation;
    protected PowerCellRechargeStationStateMachine stateMachine;

    public PowerCellRechargeStationState(PowerCellRechargeStation rechargeStation, PowerCellRechargeStationStateMachine stateMachine)
    {

        this.rechargeStation = rechargeStation;
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

    public virtual void InteractAction(Player player)
    {



    }

    public virtual string GetInteractText()
    {

        return null;

    }

}
