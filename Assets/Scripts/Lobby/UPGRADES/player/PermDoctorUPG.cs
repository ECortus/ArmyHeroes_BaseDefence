using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermDoctorUPG : UpgradeAction
{
    public override int Progress => WorkersUpgradesLVLs.DoctorHealTimeLVL;

    public override void Function()
    {
        WorkersUpgradesLVLs.UpDoctorHealTime();
    }

    public override void RefreshUI()
    {
        textInfo.text = $"-{Mathf.Round((1f - WorkersUpgradesLVLs.DoctorHealTimeMod) * 100f)} s.";
    }

    public override void RefreshGetUI()
    {
        infoImage.sprite = Sprite;
        textGet.text = $"-{Mathf.Round(WorkersUpgradesLVLs.DoctorHealTimePercentDown * 100f)} s.";
    }
}
