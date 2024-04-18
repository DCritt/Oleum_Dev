using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            name = pipe.roomName;

        }

        public void TransportActivePowerCell()
        {

            pipe.AddPowerCell(0);

        }

        public void TransportDeactivePowerCell()
        {

            pipe.AddPowerCell(1);

        }

    }

    private TransportLocationsScript transportsUIScript;
    [SerializeField] private string roomName;
    [SerializeField] private GameObject powerCellHolder;
    private int numOfPowerCells = 0;
    private static List<TransportLocation> transports = new List<TransportLocation>();

    public Player player { get; set; }

    public void AddPowerCell(int mode)
    {

        numOfPowerCells += 1;

        switch (mode)
        {

            case 0:
                Instantiate(GameManagerScript.instance.GetItemData("PowerCell(Active)").inHandGameObjectPrefab, powerCellHolder.transform.position, powerCellHolder.transform.rotation, powerCellHolder.transform);

                break;
            case 1:
                Instantiate(GameManagerScript.instance.GetItemData("PowerCell(Deactive)").inHandGameObjectPrefab, powerCellHolder.transform.position, powerCellHolder.transform.rotation, powerCellHolder.transform);

                break;

        }

    }

    public void GetPowerCell(Player player)
    {

        if (numOfPowerCells != 0)
        {

            player.Inventory.AddItem(GameManagerScript.instance.GetItemData(powerCellHolder.transform.GetChild(0).name) as HeavyUtilityItemData, powerCellHolder.transform.GetChild(0).gameObject);
            numOfPowerCells -= 1;

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
            player.StateMachine.ChangeState(player.DeadState);
            player.Inventory.stateMachine.ChangeState(player.Inventory.DeactiveState);

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

            if (transports[i].name != roomName)
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
