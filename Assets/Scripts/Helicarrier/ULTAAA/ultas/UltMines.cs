using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UltMines", menuName = "Ulta/Mines")]
public class UltMines : Ulta
{
    public override void Activate()
    {
        UltProcessing.Instance.TurnOnMines();
    }

    public override void Deactivate()
    {
        UltProcessing.Instance.TurnOffMines();
    }
}
