using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PowerCellDockStateMachine
{

    public PowerCellDockState CurrPowerCellDockState;

    public void Initialize(PowerCellDockState startingState)
    {

        CurrPowerCellDockState = startingState;
        CurrPowerCellDockState.EnterState();

    }

    public void ChangeState(PowerCellDockState newState)
    {

        CurrPowerCellDockState.ExitState();
        CurrPowerCellDockState = newState;
        CurrPowerCellDockState.EnterState();

    }

}
