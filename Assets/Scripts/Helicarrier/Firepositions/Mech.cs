using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mech : Fireposition
{
    [Space]
    [SerializeField] private MechController controller;
    [SerializeField] private RangeShootingDetector detector;

    [Space]
    [SerializeField] private GameObject unbuild;
    [SerializeField] private GameObject harmed;
    [SerializeField] private GameObject unharmed;

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
        if(Builded)
        {
            unbuild.SetActive(false);

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
        else
        {
            unbuild.SetActive(true);
            unharmed.SetActive(false);
            harmed.SetActive(false);
        }
    }
}
