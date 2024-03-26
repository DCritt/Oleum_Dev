using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCellDock : MonoBehaviour, IInteractable
{

    [SerializeField] private SpriteRenderer PowerCellDockSprite;
    [SerializeField] private Animator powerCellDockAnimator;
    [SerializeField] private AnimationClip dockingClip;
    private Player player;

    public IEnumerator dock()
    {

        powerCellDockAnimator.SetInteger("CurrAnim", 1);

        yield return new WaitForSeconds(dockingClip.length / powerCellDockAnimator.GetCurrentAnimatorStateInfo(0).speed);

        powerCellDockAnimator.SetInteger("CurrAnim", 2);

    }

    public void Interact()
    {

        player.Inventory.DeleteCurrentItem();
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(dock());

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

    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
