using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInteractActiveState : PlayerInteractState
{

    public class InteractItem
    {

        public string name;
        public int id;
        public GameObject obj;
        public Interact interact;

        public InteractItem(string name, int id, GameObject obj, Interact interact)
        {

            this.name = name;
            this.id = id;
            this.obj = obj;
            this.interact = interact;

        }

    }

    public delegate void Interact(Player player);
    private List<InteractItem> interactItems = new List<InteractItem>();

    

    public PlayerInteractActiveState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {



    }

    public override void EnterState()
    {

        base.EnterState();

        player.UIManager.SwitchInteractActive();

    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        TestInteract();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void AddInteractItem(string name, int id, GameObject obj, Interact interact)
    {

        /*for (int i = 0; i < interactItems.Count; i++)
        {

            if (interactItems[i].id == id)
            {

                return;

            }

        }*/

        interactItems.Add(new InteractItem(name, id, obj, interact));

    }

    public void RemoveInteractItem(int id)
    {

        for (int i = 0; i < interactItems.Count; i++)
        {

            if (interactItems[i].id == id)
            {

                interactItems.RemoveAt(i);

            }

        }

        if (interactItems.Count == 0)
        {

            player.StateMachine.ChangeState(player.InteractDeactiveState);

        }

    }

    public void TestInteract()
    {

        int closestInteract = FindClosestInteract();

        player.UIManager.SetInteractText(interactItems[closestInteract].name);

        if (Input.GetKeyDown(KeyCode.E))
        {

            interactItems[closestInteract].interact.Invoke(player);

        }

    }

    public int FindClosestInteract()
    {

        int closestIndex = 0;
        float closestDistance = (float)(Math.Abs(Math.Pow((player.transform.position.x - interactItems[0].obj.transform.position.x), 2) + Math.Pow((player.transform.position.y - interactItems[0].obj.transform.position.y), 2)));

        for (int i = 1; i < interactItems.Count; i++)
        {
            
            float currDistance = (float)(Math.Abs(Math.Pow((player.transform.position.x - interactItems[i].obj.transform.position.x), 2) + Math.Pow((player.transform.position.y - interactItems[i].obj.transform.position.y), 2)));

            if (currDistance < closestDistance)
            {

                closestIndex = i;
                closestDistance = currDistance;

            }

        }

        return closestIndex;

    }

}
