using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerCellRechargeStationStateMachine
{

    public PowerCellRechargeStationState CurrPowerCellRechargeStationState;

    public void Initialize(PowerCellRechargeStationState startingState)
    {

        CurrPowerCellRechargeStationState = startingState;
        CurrPowerCellRechargeStationState.EnterState();

    }

    public void ChangeState(PowerCellRechargeStationState newState)
    {

        CurrPowerCellRechargeStationState.ExitState();
        CurrPowerCellRechargeStationState = newState;
        CurrPowerCellRechargeStationState.EnterState();

    }

}
