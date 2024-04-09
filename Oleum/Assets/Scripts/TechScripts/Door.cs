using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour, IInteractable
{

    public Player player { get; set; }

    [SerializeField] private bool locked = false;

    private Animator _doorAnimator;
    [SerializeField] private BoxCollider2D _boxCollider;

    private string _previousAnimation;

    private void Start()
    {

        _doorAnimator = GetComponent<Animator>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        /*if (collision.gameObject.CompareTag("Player"))
        {

            player = collision.GetComponent<Player>();
            player.StateMachine.ChangeState(player.InteractActiveState);
            player.InteractActiveState.AddInteractItem(gameObject.name, this.GetInstanceID(), gameObject, Interact);

        }*/

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        /*if (collision.gameObject.CompareTag("Player"))
        {

            player?.InteractActiveState.RemoveInteractItem(this.GetInstanceID());

        }*/

    }

    public void Unlock()
    {

        locked = false;

    }

    public string GetInteractText(Player player)
    {

        return "Use Door";

    }

    public void Interact(Player player)
    {

        if (!(locked))
        {

            if (_doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("DoorClose"))
            {

                _doorAnimator.ResetTrigger("close");
                _doorAnimator.SetTrigger("open");

            }
            else if (_doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("DoorOpen"))
            {

                _doorAnimator.ResetTrigger("open");
                _doorAnimator.SetTrigger("close");

            }
            else if (_previousAnimation == "DoorOpen")
            {

                _doorAnimator.ResetTrigger("open");
                _doorAnimator.SetTrigger("close");

            }
            else
            {

                _doorAnimator.ResetTrigger("close");
                _doorAnimator.SetTrigger("open");


            }

        }

    }

    public void OnStart(Player player)
    {



    }

    public void OnEnd(Player player)
    {



    }

    public void SetColliderActive(bool state)
    {

        if (state)
        {

            _boxCollider.isTrigger = false;

        }
        else
        {

            _boxCollider.isTrigger = true;

        }

    }

    public void SetPrevious(string previous)
    {

        _previousAnimation = previous;

    }

    public void SwitchInteractActive()
    {

        player.UIManager.SwitchInteractActive();

    }

    public void SwitchInteractDeactive()
    {

        player.UIManager.SwitchInteractDeactive();

    }

}
