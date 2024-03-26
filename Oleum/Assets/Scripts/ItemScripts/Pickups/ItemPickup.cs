using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class ItemPickup : MonoBehaviour, IInteractable
{ 
    [SerializeField] private ItemData data;
    private Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            player = collision.GetComponent<Player>();
            player.StateMachine.ChangeState(player.InteractActiveState);
            player.InteractActiveState.AddInteractItem(data.displayName, this.GetInstanceID(), gameObject, Interact);

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        player?.InteractActiveState.RemoveInteractItem(this.GetInstanceID());

    }

    public void SetData(ItemData data)
    {

        this.data = data;
        this.data.SetType();
        gameObject.GetComponent<SpriteRenderer>().sprite = null;
        gameObject.name = data.displayName;
        Instantiate(data.inHandGameObjectPrefab, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);

    }

    private void Start()
    {

        if (data != null)
        {

            data.SetType();
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
            gameObject.name = data.displayName;
            Instantiate(data.inHandGameObjectPrefab, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);

        }
      
    }

    public void Interact()
    {

        

        switch(data.getType())
            {

            case 1:

                if (!(player.Inventory.IsFull(data as NormalConsumableItemData))) {

                    player.Inventory.AddItem(data as NormalConsumableItemData);
                    Destroy(gameObject);
                    Destroy(this);

                }
                break;

            case 2:

                if (!(player.Inventory.IsFull(data as NormalUtilityItemData))) {

                    player.Inventory.AddItem(data as NormalUtilityItemData);
                    Destroy(gameObject);
                    Destroy(this);

                }
                break;

            case 3:

                if (!(player.Inventory.IsFull(data as HeavyConsumableItemData)))
                {

                    player.Inventory.AddItem(data as HeavyConsumableItemData);
                    Destroy(gameObject);
                    Destroy(this);

                }
                break;              

            case 4:

                if (!(player.Inventory.IsFull(data as HeavyUtilityItemData)))
                {

                    player.Inventory.AddItem(data as HeavyUtilityItemData);
                    Destroy(gameObject);
                    Destroy(this);

                }
                break;

            }


    }

}
