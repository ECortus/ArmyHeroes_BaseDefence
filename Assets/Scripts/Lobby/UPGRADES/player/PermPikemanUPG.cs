using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermPikemanUPG : UpgradeAction
{
    public override int Progress => WorkersUpgradesLVLs.PikemanResourceLVL;

    public override void Function()
    {
        WorkersUpgradesLVLs.UpPikemanResource();
    }

    public override void RefreshUI()
    {
        textInfo.text = $"+{Mathf.Round(WorkersUpgradesLVLs.PikemanResourceMod)}";
    }

    public override void RefreshGetUI()
    {
        infoImage.sprite = Sprite;
        textGet.text = $"+{Mathf.Round(WorkersUpgradesLVLs.PikemanResourcePercentUP)}";
    }
}
