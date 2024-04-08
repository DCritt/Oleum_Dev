using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCellPipe : MonoBehaviour, IInteractable
{

    public class TransportLocation
    {

        public PowerCellPipe pipe;
        public Vector3 position;
        public Quaternion rotation;
        public string name;
        
        public TransportLocation(PowerCellPipe pipe)
        {

            this.pipe = pipe;
            position = pipe.gameObject.transform.position;
            rotation = pipe.gameObject.transform.rotation;
            name = pipe.transform.parent.parent.name;

        }

        public void TransportActivePowerCell()
        {

            pipe.AddActivePowerCell();

        }

        public void TransportDeactivePowerCell()
        {

            pipe.AddDeactivePowerCell();

        }

    }

    private Player player;
    private TransportLocationsScript transportsUIScript;
    private int numOfActivePowerCells = 0;
    private int numOfDeactivePowerCells = 0;
    private static List<TransportLocation> transports = new List<TransportLocation>();
    
    public void ButtonFunction(TransportLocation location, int option)
    {

        switch (option)
        {

            case 0:
                location.TransportActivePowerCell();

                break;
            case 1:
                location.TransportDeactivePowerCell();

                break;

        }
        player.Inventory.RemoveCurrentItem();
        player.InteractActiveState.RemoveInteractItem(this.GetInstanceID());
        player.InteractActiveState.AddInteractItem("Get PowerCell", this.GetInstanceID(), gameObject, GetPowerCell);

    }

    public void AddActivePowerCell()
    {

        numOfActivePowerCells += 1;

    }

    public void AddDeactivePowerCell() 
    {

        numOfDeactivePowerCells += 1;

    }

    public void GetPowerCell()
    {

        if (numOfActivePowerCells != 0)
        {

            player.Inventory.AddItem(GameManagerScript.instance.GetItemData("PowerCell(Active)") as HeavyUtilityItemData, null);
            numOfActivePowerCells -= 1;

        }
        else if (numOfDeactivePowerCells != 0)
        {

            player.Inventory.AddItem(GameManagerScript.instance.GetItemData("PowerCell(Deactive)") as HeavyUtilityItemData, null);
            numOfDeactivePowerCells -= 1;

        }

    }

    public void Interact()
    {

        if (player.Inventory.GetHeavyItemName() == "PowerCell(Deactive)" || player.Inventory.GetHeavyItemName() == "PowerCell(Active)")
        {

            player.StateMachine.ChangeState(player.InteractActiveState);
            player.InteractActiveState.AddInteractItem("Use PowerCellPipe", this.GetInstanceID(), gameObject, Interact);

        }
        else if ((numOfActivePowerCells != 0 || numOfDeactivePowerCells != 0) && !(player.Inventory.IsFull(GameManagerScript.instance.GetItemDataType("HeavyUtility") as HeavyItemData)))
        {

            player.StateMachine.ChangeState(player.InteractActiveState);
            player.InteractActiveState.AddInteractItem("Use PowerCellPipe", this.GetInstanceID(), gameObject, Interact);

        }

    }

    public void MakeUI()
    {

        player.UIManager.ResetInteractButtons();
        player.UIManager.ActivateInteractButtons();
        TransportLocationsScript temp = player.UIManager.GetInteractButtons();

        for (int i = 0; i < transports.Count; i++)
        {

            if (player.Inventory.GetHeavyItemName() == "PowerCell(Active)")
            {

                temp.SpawnTransportLocationButton(transports[i].name, new UnityEngine.Events.UnityAction(transports[i].TransportActivePowerCell));

            }
            else
            {

                temp.SpawnTransportLocationButton(transports[i].name, new UnityEngine.Events.UnityAction(transports[i].TransportDeactivePowerCell));

            }

        }

    }

    // Start is called before the first frame update
    void Start()
    {

        transports.Add(new TransportLocation(this));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {

            player = collision.GetComponent<Player>();

            if (player.Inventory.GetHeavyItemName() == "PowerCell(Deactive)" || player.Inventory.GetHeavyItemName() == "PowerCell(Active)")
            {

                player.StateMachine.ChangeState(player.InteractActiveState);
                player.InteractActiveState.AddInteractItem("Use PowerCellPipe", this.GetInstanceID(), gameObject, Interact);

            }
            else if ((numOfActivePowerCells != 0 || numOfDeactivePowerCells != 0) && !(player.Inventory.IsFull(GameManagerScript.instance.GetItemDataType("HeavyUtility") as HeavyItemData)))
            {

                player.StateMachine.ChangeState(player.InteractActiveState);
                player.InteractActiveState.AddInteractItem("Use PowerCellPipe", this.GetInstanceID(), gameObject, Interact);

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
