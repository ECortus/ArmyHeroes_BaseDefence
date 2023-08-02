using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mech : Fireposition
{
    [SerializeField] private MechController controller;
    [SerializeField] private RangeShootingDetector detector;

    protected override void EnableAction()
    {
        controller.SetMotor(2);
        detector.On();
    }

    protected override void DisableAction()
    {
        detector.Off();
        controller.SetMotor(0);
    }

    public override void RefreshModel()
    {

    }
}
