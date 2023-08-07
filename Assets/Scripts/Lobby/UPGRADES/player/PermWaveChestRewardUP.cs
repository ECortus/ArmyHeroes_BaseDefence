using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermWaveChestRewardUP : UpgradeAction
{
    public override int Progress => WaveRewardLVLs.LVL;

    public override void Function()
    {
        WaveRewardLVLs.UpWCR();
    }

    public override void RefreshUI()
    {
        textInfo.text = $"+{Mathf.Round(WaveRewardLVLs.WCRMod * 100f)}%";
    }

    public override void RefreshGetUI()
    {
        infoImage.sprite = Sprite;
        textGet.text = $"+{Mathf.Round(WaveRewardLVLs.WCRPercentUP * 100f)}%";
    }
}
