using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicarrierUpgrades : HP_DMG_SPD
{
    private float _bonus = 0f;
    public override float bonusHP
    {
        get
        {
            return _bonus + HelicarrierLVLs.HPMod;
        }
        set
        {
            _bonus = value;
        }
    }
}
