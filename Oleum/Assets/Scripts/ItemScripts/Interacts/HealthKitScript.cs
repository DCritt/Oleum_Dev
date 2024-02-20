using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKitScript : ItemInteractScript
{
    public override void Interact()
    {

        base.Interact();

        Player player = gameObject.GetComponentInParent<Player>();

        if (player.StateMachine.CurrPlayerHealthState != player.HealthFullState)
        {

            player?.Heal((float)25);
            player?.Inventory.DeleteCurrentItem();

        }

    }
}
