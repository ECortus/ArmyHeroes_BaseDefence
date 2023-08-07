using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermSoldierUP : UpgradeAction
{
    public override int Progress => SoldierUpgradesLVLs.LVL;

    public override void Function()
    {
        SoldierUpgradesLVLs.UpHP();
        SoldierUpgradesLVLs.UpDMG();
    }

    public override void RefreshUI()
    {
        textInfo.text = $"+{Mathf.Round(SoldierUpgradesLVLs.HPMod)}%" + "\n" + $"+{Mathf.Round(SoldierUpgradesLVLs.DMGMod)}%";
    }

    public override void RefreshGetUI()
    {
        infoImage.sprite = Sprite;
        textGet.text = $"+{Mathf.Round(SoldierUpgradesLVLs.HPPercentUP * 100f)}%" + "\n" + $"+{Mathf.Round(SoldierUpgradesLVLs.DMGPercentUP * 100f)}%";
    }
}
