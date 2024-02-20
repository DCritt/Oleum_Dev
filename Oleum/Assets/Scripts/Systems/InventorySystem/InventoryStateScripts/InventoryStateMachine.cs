using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryStateMachine
{

    public InventoryState CurrInventoryState;

    public void Initialize(InventoryState startingInventoryState)
    {

        CurrInventoryState = startingInventoryState;
        CurrInventoryState.EnterState();

    }

    public void ChangeState(InventoryState newState)
    {

        CurrInventoryState.ExitState();
        CurrInventoryState = newState;
        CurrInventoryState.EnterState();

    }

}
