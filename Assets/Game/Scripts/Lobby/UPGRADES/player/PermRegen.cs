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
        textInfo.text = $"+{System.MathF.Round(PlayerUpgradesLVLs.RegenMod * 100f, 1)} %/m";
    }

    public override void RefreshGetUI()
    {
        infoImage.sprite = Sprite;
        textGet.text = $"+{System.MathF.Round(PlayerUpgradesLVLs.RegenPercentPerSecond * 100f, 1)} %/m";
    }
}
