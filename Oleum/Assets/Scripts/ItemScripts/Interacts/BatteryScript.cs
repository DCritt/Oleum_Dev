using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryScript : ItemInteractScript
{

    public override void Interact(Player player)
    {

        base.Interact(player);

        if (player.StateMachine.CurrPlayerStaminaState != player.StaminaFullState)
        {

            player?.Rest((float)75);
            player?.Inventory.DeleteCurrentItem();

        }

    }

}
