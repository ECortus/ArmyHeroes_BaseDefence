using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeShootingDetector : NearestDetector
{
    [Space]
    [SerializeField] private Target trg;
    [SerializeField] private GunHandler shooting;

    public override bool AdditionalCondition(Detection dt)
    {
        return !dt.Died && dt.Active;
    }

    protected override void Reset()
    {
        data = null;
        trg.ResetTarget();

        shooting.Disable();
    }

    protected override void Change()
    {
        trg.SetTarget(data.transform);
        /* shooting.Disable(); */
    }

    protected override void Set()
    {
        trg.SetTarget(data.transform);
        if(!shooting.isEnable)
        {
            shooting.Enable();
        }
    }
}
