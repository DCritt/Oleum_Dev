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
        public Player player;
        
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

    private TransportLocationsScript transportsUIScript;
    private int numOfActivePowerCells = 0;
    private int numOfDeactivePowerCells = 0;
    private static List<TransportLocation> transports = new List<TransportLocation>();

    public Player player { get; set; }

    public void AddActivePowerCell()
    {

        numOfActivePowerCells += 1;

    }

    public void AddDeactivePowerCell() 
    {

        numOfDeactivePowerCells += 1;

    }

    public void GetPowerCell(Player player)
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

    public string GetInteractText(Player player)
    {

        return "Use PowerCell Pipe";

    }

    public void Interact(Player player)
    {

        if (player.Inventory.GetHeavyItemName() == "PowerCell(Active)" || player.Inventory.GetHeavyItemName() == "PowerCell(Deactive)")
        {

            MakeUI(player);

        }
        else if (!(player.Inventory.IsFull(GameManagerScript.instance.GetItemDataType("HeavyUtility") as HeavyItemData)))
        {

            GetPowerCell(player);

        }

    }

    public void OnStart(Player player)
    {



    }

    public void OnEnd(Player player)
    {



    }

    public void MakeUI(Player player)
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
        
       

    }


    private void OnTriggerExit2D(Collider2D collision)
    {


    }

}
