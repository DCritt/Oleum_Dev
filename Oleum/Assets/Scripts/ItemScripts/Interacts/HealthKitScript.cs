using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKitScript : ItemInteractScript
{
    public override void Interact(Player player)
    {

        base.Interact(player);

        if (player.StateMachine.CurrPlayerHealthState != player.HealthFullState)
        {

            player?.Heal((float)25);
            player?.Inventory.DeleteCurrentItem();

        }

    }
}
