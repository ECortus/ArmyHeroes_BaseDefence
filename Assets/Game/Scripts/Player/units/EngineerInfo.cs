using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerInfo : Detection
{
    [SerializeField] private float repairForce;

    public void Repair(Detection dt)
    {
        if(dt != null) dt.Heal(repairForce);
    }
}
