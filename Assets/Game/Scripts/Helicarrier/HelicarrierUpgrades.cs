using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicarrierUpgrades : HP_DMG_SPD
{
    public override float bonusHP
    {
        get
        {
            return _bHP + HelicarrierLVLs.HPMod;
        }
        set
        {
            _bHP = value;
        }
    }
}
