using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBall : ResourceBall
{
    protected override void AddRecourceToPlayer()
    {
        Gold.Plus((int)resourceAmount);
        base.AddRecourceToPlayer();
    }
}
