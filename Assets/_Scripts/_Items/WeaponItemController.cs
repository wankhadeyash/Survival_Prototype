using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItemController : ItemController
{

    public override void UseItem()
    {
        Debug.Log($"Using aex");
    }

    public override void DoSecondaryTask()
    {
    }


}
