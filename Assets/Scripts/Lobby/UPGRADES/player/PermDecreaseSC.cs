using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermDecreaseSC : UpgradeAction
{
    public override int Progress => PlayerUpgradesLVLs.SCLVL;

    public override void Function()
    {
        PlayerUpgradesLVLs.DecreaseSC();
    }

    public override void RefreshUI()
    {
        textInfo.text = $"0{Mathf.Round(PlayerUpgradesLVLs.SCMod)} s.";
    }

    public override void RefreshGetUI()
    {
        infoImage.sprite = Sprite;
        textGet.text = $"-{Mathf.Round(PlayerUpgradesLVLs.SCPercentDecrease)} s.";
    }
}
