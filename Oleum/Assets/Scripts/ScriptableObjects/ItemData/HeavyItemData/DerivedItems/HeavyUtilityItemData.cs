using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory Heavy Utility Item Data")]
public class HeavyUtilityItemData : HeavyItemData
{

    public override void SetType()
    {

        base.SetType();

        type = 4;

    }

}
  
