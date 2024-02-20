using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Inventory Heavy Consumable Item Data")]
public class HeavyConsumableItemData : HeavyItemData
{
    public override void SetType()
    {

        base.SetType();

        type = 3;

    }

}
