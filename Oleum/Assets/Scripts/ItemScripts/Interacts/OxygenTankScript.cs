using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenTankScript : ItemInteractScript
{
    public override void Interact(Player player)
    {

        base.Interact(player);

        if (player.StateMachine.CurrPlayerOxygenState != player.OxygenFullState)
        {

            player?.Breath((float)50);
            player?.Inventory.DeleteCurrentItem();

        }

    }
}
