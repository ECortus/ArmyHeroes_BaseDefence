using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnitBall : ResourceBall
{
    protected override void AddRecourceToPlayer()
    {
        ResourcePool.Instance.SetMoveAllType(ResourceType.Exp);
        base.AddRecourceToPlayer();
    }
}
