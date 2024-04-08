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

        public void TransportPowerCell()
        {

            

        }

    }

    private Player player;
    private TransportLocationsScript transportsUIScript;
    private static List<TransportLocation> transports = new List<TransportLocation>();
    

    public void Interact()
    {

        MakeUI();

    }

    public void MakeUI()
    {

        player.UIManager.ResetInteractButtons();
        player.UIManager.ActivateInteractButtons();
        TransportLocationsScript temp = player.UIManager.GetInteractButtons();

        for (int i = 0; i < transports.Count; i++)
        {

            temp.SpawnTransportLocationButton(transports[i].name, new UnityEngine.Events.UnityAction(transports[i].TransportPowerCell));

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
