using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierInfo : Detection
{
    [Space]
    [SerializeField] private FirepositionUser FPuser;
    [SerializeField] private SoldierDetector detector;

    public override void Resurrect()
    {
        base.Resurrect();

        if(FPuser != null)
        {
            if(FPuser.Pool())
            {
                return;
            }
        }

        detector.On();
    }
}
