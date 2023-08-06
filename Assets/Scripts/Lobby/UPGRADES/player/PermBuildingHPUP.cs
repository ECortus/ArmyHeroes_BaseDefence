using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermBuildingHPUP : UpgradeAction
{
    public override int Progress => BuildingUpgradesLVLs.HPLVL;

    public override void Function()
    {
        BuildingUpgradesLVLs.UpHP();
    }

    public override void RefreshUI()
    {
        textInfo.text = $"+{Mathf.Round(BuildingUpgradesLVLs.HPMod)}%";
    }

    public override void RefreshGetUI()
    {
        infoImage.sprite = Sprite;
        textGet.text = $"+{Mathf.Round(BuildingUpgradesLVLs.HPPercentUP)}%";
    }
}
