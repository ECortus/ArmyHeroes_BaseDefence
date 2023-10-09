using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpMinesRange : UpgradeAction
{
    [SerializeField] private UltMines ulta;

    public override int Progress => ulta.PowerLVL;

    public override void Function()
    {
        ulta.UpPowerLVL();
    }

    public override void RefreshUI()
    {
        textInfo.text = $"+{ulta.RangeMod} m";
    }

    public override void RefreshGetUI()
    {
        infoImage.sprite = Sprite;
        textGet.text = $"+{ulta.rangeUpPerLVL} m";
    }
}
