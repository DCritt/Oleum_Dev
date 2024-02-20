using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory Utility Item Data")]
public class NormalUtilityItemData : NormalItemData
{
    public override void SetType()
    {

        base.SetType();

        type = 2;

    }

}
