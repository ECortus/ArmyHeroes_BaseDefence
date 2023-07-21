using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackPlayer : NearestDetector
{
    [Space]
    [SerializeField] private Player controller;
    [SerializeField] private Shooting shooting;

    public override bool AdditionalCondition(Detection dt)
    {
        return !dt.Died && dt.Active;
    }

    protected override void Reset()
    {
        data = null;
        controller.ResetTarget();
        controller.takeControl = false;

        shooting.Disable();
    }

    protected override void Change()
    {
        controller.SetTarget(data.transform);
        controller.takeControl = false;

        /* shooting.Disable(); */
    }

    protected override void Set()
    {
        controller.SetTarget(data.transform);
        if(!shooting.isEnable)
        {
            shooting.Enable();
        }
    }
}
