using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesDetectorForShooter : Detector
{
    public override HumanoidController humanController => controller;
    
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
        if(data != null)
        {
            controller.SetTarget(data.transform);
            controller.takeControl = TakeControl;
        }

        /* shooting.Disable(); */
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
