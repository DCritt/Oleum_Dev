using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCellRechargeStation : MonoBehaviour, IInteractable
{
    public Player player { get; set; }

    public PowerCellRechargeStationStateMachine stateMachine;

    public PowerCellRechargeStationEmptyState emptyState;
    public PowerCellRechargeStationFullState fullState;

    public string GetInteractText(Player player)
    {

        return stateMachine.CurrPowerCellRechargeStationState.GetInteractText();
        
    }

    public void Interact(Player player)
    {

        stateMachine.CurrPowerCellRechargeStationState.InteractAction(player);

    }

    public void OnEnd(Player player)
    {
        


    }

    public void OnStart(Player player)
    {
       


    }


    // Start is called before the first frame update
    void Start()
    {

        stateMachine.Initialize(emptyState);

    }

    void Awake()
    {

        stateMachine = new PowerCellRechargeStationStateMachine();

        emptyState = new PowerCellRechargeStationEmptyState(this, stateMachine);
        fullState = new PowerCellRechargeStationFullState(this, stateMachine);

    }

    // Update is called once per frame
    void Update()
    {

        stateMachine.CurrPowerCellRechargeStationState.FrameUpdate();
        
    }

    void FixedUpdate()
    {

        stateMachine.CurrPowerCellRechargeStationState.PhysicsUpdate();

    } 

}
