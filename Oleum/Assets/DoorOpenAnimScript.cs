using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenAnimScript : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.GetComponentInParent<Door>().SetColliderActive(false);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.SetFloat("timer", (animator.GetFloat("timer") + Time.deltaTime));

    }

     //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.GetComponent<Door>().SetPrevious("DoorOpen");
        animator.SetFloat("timer", 0);

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    /*override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Debug.Log("openEnd");
        animator.GetComponentInParent<Door>().SetColliderActive(true);

    }*/

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
