using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{

    public PlayerState CurrPlayerState { get; set; }
    public PlayerHealthState CurrPlayerHealthState { get; set; }
    public PlayerStaminaState CurrPlayerStaminaState { get; set; }
    public PlayerOxygenState CurrPlayerOxygenState { get; set; }
    public PlayerInteractState CurrPlayerInteractState { get; set; }
    public void Initialize(PlayerState startingState, PlayerHealthState startingHealthState, PlayerStaminaState startingStaminaState, PlayerOxygenState startingOxygenState, PlayerInteractState startingInteractState)
    {

        CurrPlayerState = startingState;
        CurrPlayerHealthState = startingHealthState;
        CurrPlayerStaminaState = startingStaminaState;
        CurrPlayerOxygenState = startingOxygenState;
        CurrPlayerInteractState = startingInteractState;
        CurrPlayerState.EnterState();
        CurrPlayerHealthState.EnterState();
        CurrPlayerStaminaState.EnterState();
        CurrPlayerOxygenState.EnterState();
        CurrPlayerInteractState.EnterState();

    }

    public void ChangeState(PlayerState newState)
    {

        CurrPlayerState.ExitState();
        CurrPlayerState = newState;
        CurrPlayerState.EnterState();

    }

    public void ChangeState(PlayerHealthState newState)
    {

        CurrPlayerHealthState.ExitState();
        CurrPlayerHealthState = newState;
        CurrPlayerHealthState.EnterState();

    }

    public void ChangeState(PlayerStaminaState newState)
    {

        CurrPlayerStaminaState.ExitState();
        CurrPlayerStaminaState = newState;
        CurrPlayerStaminaState.EnterState();

    }

    public void ChangeState(PlayerOxygenState newState)
    {

        CurrPlayerOxygenState.ExitState();
        CurrPlayerOxygenState = newState;
        CurrPlayerOxygenState.EnterState();

    }

    public void ChangeState(PlayerInteractState newState)
    {

        CurrPlayerInteractState.ExitState();
        CurrPlayerInteractState = newState;
        CurrPlayerInteractState.EnterState();
    }

}
