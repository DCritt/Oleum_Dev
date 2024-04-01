using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryState
{

    protected Inventory inventory;
    protected Player player;
    protected InventoryStateMachine stateMachine;

    public InventoryState(Inventory inventory, Player player, InventoryStateMachine stateMachine)
    {

        this.inventory = inventory;
        this.player = player;
        this.stateMachine = stateMachine;

    }

    public virtual void EnterState()
    {



    }

    public virtual void ExitState()
    {



    }

    public virtual void FrameUpdate()
    {



    }

    public virtual void PhysicsUpdate()
    {



    }

    public virtual void AddItem(NormalItemData data, GameObject obj)
    {



    }

    public virtual void AddItem(HeavyItemData data, GameObject obj)
    {

        if (inventory.Slots[4] == null)
        {

            inventory.Slots[4] = new HeavyItem(data as HeavyItemData, player, obj);
            SetCurrentItem(4);
            player.UIManager.ResetSelect();
            stateMachine.ChangeState(inventory.LockedState);
            player.PlayerBodyAnim.SetInteger("CurrAnim", 2);

        }

    }

    public virtual void RemoveCurrentItem()
    {



    }

    public virtual void DeleteCurrentItem()
    {

       

    }

    public virtual void SetCurrentItem(int item)
    {



    }

    public virtual void UseCurrentItem()
    {

        inventory.Slots[inventory.currentItem]?.Interact();

    }



}
