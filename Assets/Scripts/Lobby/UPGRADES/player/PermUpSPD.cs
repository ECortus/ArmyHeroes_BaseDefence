using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermUpSPD : UpgradeAction
{
    public override int Progress => PlayerUpgradesLVLs.SPDLVL;

    public override void Function()
    {
        PlayerUpgradesLVLs.UpSPD();
    }

    public override void RefreshUI()
    {
        textInfo.text = $"+{Mathf.Round(PlayerUpgradesLVLs.SPDMod)}%";
    }

    public override void RefreshGetUI()
    {
        infoImage.sprite = Sprite;
        textGet.text = $"+{Mathf.Round(PlayerUpgradesLVLs.SPDPercentUP)}%";
    }
}
