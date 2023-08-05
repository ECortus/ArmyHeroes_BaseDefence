using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpUltaDuration : UpgradeAction
{
    [SerializeField] private Ulta ulta;

    public override int Progress => ulta.DurationLVL;

    public override void Function()
    {
        ulta.IncreaseDuration();
    }

    public override void RefreshUI()
    {
        textInfo.text = $"+{ulta.DurationMod} s.";
    }

    public override void RefreshGetUI()
    {
        infoImage.sprite = Sprite;
        textGet.text = $"+{ulta.durationUpPerLvl} s.";
    }
}
