using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKitBall : ResourceBall
{
    protected override void AddRecourceToPlayer()
    {
        PlayerInfo.Instance.Heal(resourceAmount);
        base.AddRecourceToPlayer();
    }
}