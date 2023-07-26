using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UltEconomy", menuName = "Ulta/Economy")]
public class UltEconomy : Ulta
{
    public override void Activate()
    {
        UltProcessing.Instance.TurnOnEconomy();
    }

    public override void Deactivate()
    {
        UltProcessing.Instance.TurnOffEconomy();
    }
}
