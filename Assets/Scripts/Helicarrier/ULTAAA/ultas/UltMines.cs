using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UltMines", menuName = "Ulta/Mines")]
public class UltMines : Ulta
{
    [Space]
    public int Count = 10;
    public float Damage = 25f;

    [Space]
    [SerializeField] private float defaultRange = 3f;
    public float rangeUpPerLVL = 0.25f;

    public float RangeMod
    {
        get
        {
            return rangeUpPerLVL * PowerLVL;
        }
    }

    public float Range
    {
        get
        {
            return defaultRange + RangeMod;
        }
    }

    public override void Activate()
    {
        UltProcessing.Instance.TurnOnMines();
    }

    public override void Deactivate()
    {
        UltProcessing.Instance.TurnOffMines();
    }
}
