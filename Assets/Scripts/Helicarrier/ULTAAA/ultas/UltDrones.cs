using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UltDrones", menuName = "Ulta/Drones")]
public class UltDrones : Ulta
{
    public override void Activate()
    {
        UltProcessing.Instance.TurnOnDrones();
    }

    public override void Deactivate()
    {
        UltProcessing.Instance.TurnOffDrones();
    }
}
