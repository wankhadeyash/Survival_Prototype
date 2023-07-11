using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlankBrains.Inventory;
public class WeaponItemController : ItemController
{

    protected override void UseItem()
    {
        Debug.Log($"Using aex");
    }

    protected override void DoSecondaryTask()
    {

    }

}
