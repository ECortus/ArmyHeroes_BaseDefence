using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mech : Fireposition
{
    [Space]
    [SerializeField] private MechController controller;
    [SerializeField] private RangeShootingDetector detector;

    [Space]
    [SerializeField] private GameObject unharmed;
    [SerializeField] private GameObject harmed;

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
        if(HP > 0f)
        {
            unharmed.SetActive(true);
            harmed.SetActive(false);
        }
        else
        {
            unharmed.SetActive(false);
            harmed.SetActive(true);
        }
    }
}
