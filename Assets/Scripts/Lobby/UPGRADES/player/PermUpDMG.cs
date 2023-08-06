using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PermUpDMG : UpgradeAction
{
    public override int Progress => PlayerUpgradesLVLs.DMGLVL;

    public override void Function()
    {
        PlayerUpgradesLVLs.UpDMG();
    }

    public override void RefreshUI()
    {
        textInfo.text = $"+{Mathf.Round(PlayerUpgradesLVLs.DMGMod)}%";
    }

    public override void RefreshGetUI()
    {
        infoImage.sprite = Sprite;
        textGet.text = $"+{Mathf.Round(PlayerUpgradesLVLs.DMGPercentUP)}%";
    }
}
