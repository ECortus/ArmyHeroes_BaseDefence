using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyProcessing : UltProcessing_IEnumerator
{
    [SerializeField] private UltEconomy info;

    public override IEnumerator Process()
    {
        int gold = info.GoldPerCrystal * Statistics.Crystal;

        Crystal.Minus(Statistics.Crystal);
        Gold.Plus(gold);

        yield return null;
    }

    public override IEnumerator Deprocess()
    {
        yield return null;
    }
}
