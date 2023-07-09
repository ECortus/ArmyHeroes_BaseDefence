using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceBall : RecourceBall
{
    public override void AddRecourceToPlayer()
    {
        Experience.Plus(recourceAmount);
        base.AddRecourceToPlayer();
    }
}
