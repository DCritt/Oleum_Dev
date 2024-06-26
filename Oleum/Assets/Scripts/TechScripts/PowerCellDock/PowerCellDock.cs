using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCellDock : MonoBehaviour, IInteractable
{

    [SerializeField] private SpriteRenderer PowerCellDockSprite;
    [SerializeField] private Animator powerCellDockAnimator;
    [SerializeField] private AnimationClip dockingClip;
    private GameObject powerCell;
    public Player player { get; set; }

    public PowerCellDockStateMachine stateMachine;

    public PowerCellDockEmptyState emptyState;
    public PowerCellDockRepairedState repairedState;
    public PowerCellDockDeadState deadState;

    private IEnumerator dock;

    private void Awake()
    {

        stateMachine = new PowerCellDockStateMachine();

        emptyState = new PowerCellDockEmptyState(this, stateMachine);
        repairedState = new PowerCellDockRepairedState(this, stateMachine);
        deadState = new PowerCellDockDeadState(this, stateMachine);

    }

    private void Start()
    {

        stateMachine.Initialize(emptyState);
        dock = stateMachine.CurrPowerCellDockState.Dock();

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

    public string GetInteractText(Player player)
    {

        return "Use PowerCell Dock";

    }

    public void Interact(Player player)
    {

        stateMachine.CurrPowerCellDockState.InteractAction(player);

    }

    public void OnStart(Player player)
    {



    }

    public void OnEnd(Player player)
    {



    }

    public void StartDock()
    {

        dock = stateMachine.CurrPowerCellDockState.Dock();
        StartCoroutine(dock);

    }

    public GameObject GetPlayerPowerCell()
    {

        return powerCell;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        /*if (collision.tag == "Player")
        {

            player = collision.GetComponent<Player>();

            if (player.Inventory.GetHeavyItemName() == "PowerCell(Deactive)" || player.Inventory.GetHeavyItemName() == "PowerCell(Active)" || stateMachine.CurrPowerCellDockState == deadState)
            {

                player.StateMachine.ChangeState(player.InteractActiveState);
                player.InteractActiveState.AddInteractItem("Use PowerCellDock", this.GetInstanceID(), gameObject, Interact);

            } 

        }*/
        if (collision.tag == "PowerCell")
        {

            powerCell = collision.gameObject;

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        /*if (collision.tag == "Player")
        {

            player = collision.gameObject.GetComponent<Player>();

            player?.InteractActiveState.RemoveInteractItem(this.GetInstanceID());

        }*/

    }

}
