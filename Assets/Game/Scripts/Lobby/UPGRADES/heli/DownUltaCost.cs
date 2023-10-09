using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DownUltaCost : UpgradeAction
{
    [SerializeField] private Ulta ulta;

    public override int Progress => ulta.CostLVL;

    public override void Function()
    {
        ulta.DecreaseCost();
    }

    public override void RefreshUI()
    {
        textInfo.text = $"-{Mathf.Round((1f - ulta.CostMod) * 100f)}%";
    }

    public override void RefreshGetUI()
    {
        infoImage.sprite = Sprite;
        textGet.text = $"-{ulta.costDownPerLvlOnPercent}%";
    }
}
