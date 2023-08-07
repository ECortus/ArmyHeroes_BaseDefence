using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermRegen : UpgradeAction
{
    public override int Progress => PlayerUpgradesLVLs.RegenLVL;

    public override void Function()
    {
        PlayerUpgradesLVLs.UpRegen();
    }

    public override void RefreshUI()
    {
        textInfo.text = $"+{Mathf.Round(PlayerUpgradesLVLs.RegenMod * 100f)} %/s";
    }

    public override void RefreshGetUI()
    {
        infoImage.sprite = Sprite;
        textGet.text = $"+{Mathf.Round(PlayerUpgradesLVLs.RegenPercentPerSecond * 100f)} %/s";
    }
}
