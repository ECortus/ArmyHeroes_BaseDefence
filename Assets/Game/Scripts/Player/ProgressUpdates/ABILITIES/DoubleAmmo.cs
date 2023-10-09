using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPB_DoubleAmmo", menuName = "NPB-s/Double Ammo")]
public class DoubleAmmo : NewProgressBonus
{
    public override void Apply()
    {
        PlayerUpgrades.Instance.ShootingUpgrades.AddAmmoPerShotMultiple(2);
    }

    public override void Cancel()
    {
        PlayerUpgrades.Instance.ShootingUpgrades.ResetAmmoPerShotMultiple();
    }
}
