using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PowerCellScript : ItemInteractScript
{

    [SerializeField] private Light2D spotLight;
    [SerializeField] private bool activated;
    [SerializeField] private Animator animator;

    [ContextMenu("ToggleActive")]
    public void ToggleActive()
    {

        Debug.Log("toggle");

        activated = !activated;

        if (activated)
        {

            animator.SetInteger("CurrAnim", 1);

        }
        else
        {

            animator.SetInteger("CurrAnim", 0);

        }

    }

    public void ToggleLight()
    {

        spotLight.gameObject.SetActive(!(spotLight.gameObject.activeInHierarchy));

    }

    public override void Interact()
    {

        base.Interact();


    }

    private void Start()
    {

        Debug.Log("start");

        animator.keepAnimatorStateOnDisable = true;

        if (activated)
        {

            animator.SetInteger("CurrAnim", 1);

        }
        else
        {

            animator.SetInteger("CurrAnim", 0);

        }

    }
}
