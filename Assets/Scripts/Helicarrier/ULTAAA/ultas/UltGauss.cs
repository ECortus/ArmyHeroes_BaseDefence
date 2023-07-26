using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UltGauss", menuName = "Ulta/Gauss")]
public class UltGauss : Ulta
{
    public override void Activate()
    {
        UltProcessing.Instance.TurnOnGauss();
    }

    public override void Deactivate()
    {
        UltProcessing.Instance.TurnOffGauss();
    }
}
