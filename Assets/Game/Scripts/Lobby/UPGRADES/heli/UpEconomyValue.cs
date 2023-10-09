using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpEconomyValue : UpgradeAction
{
    [SerializeField] private UltEconomy ulta;

    public override int Progress => ulta.PowerLVL;

    public override void Function()
    {
        ulta.UpPowerLVL();
    }

    public override void RefreshUI()
    {
        textInfo.text = $"+{ulta.GoldAmountMod} G";
    }

    public override void RefreshGetUI()
    {
        infoImage.sprite = Sprite;
        textGet.text = $"+{ulta.GoldAmountUpPerLVL} G";
    }
}
