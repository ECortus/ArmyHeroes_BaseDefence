using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UltEconomy", menuName = "Ulta/Economy")]
public class UltEconomy : Ulta
{
    [Space]
    [SerializeField] private int DefaultGoldPerCrystal = 10;
    [SerializeField] private int GoldAmountUpPerLVL = 15;

    public int GoldPerCrystal
    {
        get
        {
            return DefaultGoldPerCrystal + GoldAmountUpPerLVL * PowerLVL;
        }
    }

    public override void Activate()
    {
        UltProcessing.Instance.TurnOnEconomy();
    }

    public override void Deactivate()
    {
        UltProcessing.Instance.TurnOffEconomy();
    }
}
