using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UltShield", menuName = "Ulta/Shield")]
public class UltShield : Ulta
{
    public override void Activate()
    {
        UltProcessing.Instance.TurnOnShield();
    }

    public override void Deactivate()
    {
        UltProcessing.Instance.TurnOffShield();
    }
}
