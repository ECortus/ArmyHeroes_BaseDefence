using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermEngineerUPG : UpgradeAction
{
    public override int Progress => WorkersUpgradesLVLs.EngineerRepairSpeedLVL;

    public override void Function()
    {
        WorkersUpgradesLVLs.UpEngineerRepairSpeed();
    }

    public override void RefreshUI()
    {
        textInfo.text = $"-{Mathf.Round((1f - WorkersUpgradesLVLs.EngineerRepairSpeedMod) * 100f)}%";
    }

    public override void RefreshGetUI()
    {
        infoImage.sprite = Sprite;
        textGet.text = $"-{Mathf.Round(WorkersUpgradesLVLs.EngineerRepairSpeedPercentUP * 100f)}%";
    }
}
