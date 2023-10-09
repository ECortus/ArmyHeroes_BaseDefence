using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : HP_DMG_SPD
{
    public static PlayerUpgrades Instance { get; set; }
    void Awake() => Instance = this;

    public override float bonusHP
    {
        get
        {
            return _bHP + PlayerSkin.HPMod;
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
            return _bDMG + PlayerUpgradesLVLs.DMGMod + PlayerSkin.DMGMod;
        }
        set
        {
            _bDMG = value;
        }
    }

    public override float bonusSPD
    {
        get
        {
            return _bSPD + PlayerUpgradesLVLs.SPDMod;
        }
        set
        {
            _bSPD = value;
        }
    }

    public ShootingUpgrades ShootingUpgrades;

    public void SetPermanentBonuses()
    {
        ShootingUpgrades.AddDecreaseSC(PlayerUpgradesLVLs.SCMod);
    }
}