using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCellDock : MonoBehaviour, IInteractable
{

    [SerializeField] private SpriteRenderer PowerCellDockSprite;
    [SerializeField] private Animator powerCellDockAnimator;
    [SerializeField] private AnimationClip dockingClip;
    private Player player;

    public PowerCellDockStateMachine stateMachine;

    public PowerCellDockEmptyState emptyState;
    public PowerCellDockRepairedState repairedState;

    private void Awake()
    {

        stateMachine = new PowerCellDockStateMachine();

        emptyState = new PowerCellDockEmptyState(this, stateMachine);
        repairedState = new PowerCellDockRepairedState(this, stateMachine);

    }

    private void Start()
    {

        stateMachine.Initialize(emptyState);

    }

    private void Update()
    {

        stateMachine.CurrPowerCellDockState.FrameUpdate();

    }

    private void FixedUpdate()
    {

        stateMachine.CurrPowerCellDockState.PhysicsUpdate();

    }

    public Player GetPlayer()
    {

        return player;

    } 

    public AnimationClip GetDockingClip()
    {

        return dockingClip;

    }

    public Animator GetPowerCellDockAnimator()
    {

        return powerCellDockAnimator;

    }

    public void Interact()
    {

        StartCoroutine(stateMachine.CurrPowerCellDockState.Dock());

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {

            player = collision.GetComponent<Player>();

            if (player.Inventory.GetHeavyItemName() == "PowerCell")
            {

                player.StateMachine.ChangeState(player.InteractActiveState);
                player.InteractActiveState.AddInteractItem("PowerCell", this.GetInstanceID(), gameObject, Interact);

            } 

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            player = collision.gameObject.GetComponent<Player>();

            player?.InteractActiveState.RemoveInteractItem(this.GetInstanceID());

        }
    }

}
