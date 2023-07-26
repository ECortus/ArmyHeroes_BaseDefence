using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UltOrbHit", menuName = "Ulta/OrbHit")]
public class UltOrbHit : Ulta
{
    public override void Activate()
    {
        UltProcessing.Instance.TurnOnOrbHit();
    }

    public override void Deactivate()
    {
        UltProcessing.Instance.TurnOffOrbHit();
    }
}
