using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPopUpAction : PopUpAction
{
    [SerializeField] private int GoldPerWave = 75;
    private int GoldReward => GoldPerWave * (EndLevelStats.Instance.WaveIndex > 0 ? EndLevelStats.Instance.WaveIndex : 1);

    public override void Do()
    {
        Gold.Plus(GoldReward);
    }
}
