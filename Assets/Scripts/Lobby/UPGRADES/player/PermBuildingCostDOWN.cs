using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermBuildingCostDOWN : UpgradeAction
{
    public override int Progress => BuildingUpgradesLVLs.CostLVL;

    public override void Function()
    {
        BuildingUpgradesLVLs.DownCost();
    }

    public override void RefreshUI()
    {
        textInfo.text = $"-{Mathf.Round(1f - BuildingUpgradesLVLs.CostMod)}%";
    }

    public override void RefreshGetUI()
    {
        infoImage.sprite = Sprite;
        textGet.text = $"-{Mathf.Round(1f - BuildingUpgradesLVLs.CostPercentDown)}%";
    }
}
