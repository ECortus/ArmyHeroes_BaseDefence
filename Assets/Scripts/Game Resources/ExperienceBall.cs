using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceBall : ResourceBall
{
    public override void AddRecourceToPlayer()
    {
        Experience.Plus(resourceAmount);
        base.AddRecourceToPlayer();
    }
}
