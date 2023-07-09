using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesDetectorForShooter : Detector
{
    [Space]
    public HumanoidController controller;
    public Shooting shooting;
    public bool TakeControl = false;

    protected override void Reset()
    {
        data = null;
        controller.ResetTarget();
        controller.takeControl = false;

        shooting.Disable();
    }

    protected override void Change()
    {
        controller.ResetTarget();
        controller.takeControl = false;

        shooting.Disable();
    }

    protected override void Set()
    {
        if(!shooting.isEnable)
        {
            controller.SetTarget(data.transform);
            controller.takeControl = TakeControl;
            
            shooting.Enable();
        }
    }
}
