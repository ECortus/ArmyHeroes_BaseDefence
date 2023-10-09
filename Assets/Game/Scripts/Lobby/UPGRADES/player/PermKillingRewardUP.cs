using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermKillingRewardUP : UpgradeAction
{
    public override int Progress => KillingRewardLVLs.LVL;

    public override void Function()
    {
        KillingRewardLVLs.UpEnemyKillingEXPReward();
    }

    public override void RefreshUI()
    {
        textInfo.text = $"+{Mathf.Round(KillingRewardLVLs.EnemyKillingEXPRewardMod * 100f)}%";
    }

    public override void RefreshGetUI()
    {
        infoImage.sprite = Sprite;
        textGet.text = $"+{Mathf.Round(KillingRewardLVLs.EnemyKillingEXPRewardPercentUP * 100f)}%";
    }
}
