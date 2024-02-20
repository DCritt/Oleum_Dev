using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryScript : ItemInteractScript
{

    public override void Interact()
    {

        base.Interact();

        Player player = gameObject.GetComponentInParent<Player>();

        if (player.StateMachine.CurrPlayerStaminaState != player.StaminaFullState)
        {

            player?.Rest((float)75);
            player?.Inventory.DeleteCurrentItem();

        }

    }
}
