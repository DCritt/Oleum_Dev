using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory Consumable Item Data")]
public class NormalConsumableItemData : NormalItemData
{
    public override void SetType()
    {

        base.SetType();

        type = 1;

    }

}
