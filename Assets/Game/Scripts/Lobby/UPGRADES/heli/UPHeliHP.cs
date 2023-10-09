using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UPHeliHP : UpgradeAction
{
    public override int Progress => HelicarrierLVLs.HPLVL;

    public override void Function()
    {
        HelicarrierLVLs.UpHPLevel();
    }

    public override void RefreshUI()
    {
        textInfo.text = $"+{HelicarrierLVLs.HPMod}%";
    }
}
