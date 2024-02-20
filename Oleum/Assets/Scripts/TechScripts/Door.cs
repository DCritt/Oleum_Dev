using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour, IInteractable
{

    private Player player;

    private Animator _doorAnimator;
    private BoxCollider2D _boxCollider;

    private string _previousAnimation;

    private void Start()
    {

        _doorAnimator = GetComponent<Animator>();
        _boxCollider = GameObject.FindGameObjectWithTag("door").GetComponent<BoxCollider2D>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        player = collision.GetComponent<Player>();
        player.StateMachine.ChangeState(player.InteractActiveState);
        player.InteractActiveState.SetInteract(Interact);

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        player.InteractActiveState.SetInteract(null);
        player.StateMachine.ChangeState(player.InteractDeactiveState);

    }

    public void Interact()
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
