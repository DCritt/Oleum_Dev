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
    private GameObject obj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            player = collision.GetComponent<Player>();
            player.StateMachine.ChangeState(player.InteractActiveState);
            player.InteractActiveState.AddInteractItem(data.displayName, this.GetInstanceID(), gameObject, Interact);

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

            player?.InteractActiveState.RemoveInteractItem(this.GetInstanceID());

        }

    }

    public void SetData(ItemData data, GameObject obj)
    {

        this.data = data;
        this.obj = obj;
        this.data.SetType();
        gameObject.GetComponent<SpriteRenderer>().sprite = null;
        gameObject.name = data.displayName;
        obj.transform.parent = this.transform;
        obj.transform.localPosition = Vector3.zero;
        //Instantiate(data.inHandGameObjectPrefab, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);

    }

    private void Start()
    {

        if (data != null && gameObject.transform.childCount == 0)
        {

            Debug.Log("hello");

            data.SetType();
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
            gameObject.name = data.displayName;
            obj = Instantiate(data.inHandGameObjectPrefab, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);

        }
      
    }

    public void Interact()
    {

        Debug.Log(data.getType());

        switch(data.getType())
            {


            case 1:

                if (!(player.Inventory.IsFull(data as NormalConsumableItemData))) {

                    player.Inventory.AddItem(data as NormalConsumableItemData, obj);
                    Destroy(gameObject);
                    Destroy(this);

                }
                break;

            case 2:

                if (!(player.Inventory.IsFull(data as NormalUtilityItemData))) {

                    player.Inventory.AddItem(data as NormalUtilityItemData, obj);
                    Destroy(gameObject);
                    Destroy(this);

                }
                break;

            case 3:

                if (!(player.Inventory.IsFull(data as HeavyConsumableItemData)))
                {

                    player.Inventory.AddItem(data as HeavyConsumableItemData, obj);
                    Destroy(gameObject);
                    Destroy(this);

                }
                break;              

            case 4:

                if (!(player.Inventory.IsFull(data as HeavyUtilityItemData)))
                {

                    player.Inventory.AddItem(data as HeavyUtilityItemData, obj);
                    Destroy(gameObject);
                    Destroy(this);

                }
                break;

            }


    }

}
