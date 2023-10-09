using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierUpgrades : HP_DMG_SPD
{
    [SerializeField] private SoldierSkinController skins;
    [SerializeField] private UpgradeSoldiers upgradeSoldiers;

    public override float bonusHP
    {
        get
        {
            return _bHP + SoldierUpgradesLVLs.HPMod;
        }
        set
        {
            _bHP = value;
        }
    }

    public override float bonusDMG
    {
        get
        {
            return _bDMG + SoldierUpgradesLVLs.DMGMod;
        }
        set
        {
            _bDMG = value;
        }
    }

    public override void AdditionalActionOnUpgrade()
    {
        skins.Refresh();
    }

    void Start()
    {
        AddHPPercent(upgradeSoldiers.PlusHP);
        AddDMGPercent(upgradeSoldiers.PlusDMG);
    }
}