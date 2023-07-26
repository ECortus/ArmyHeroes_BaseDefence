using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPB_SpecialAmmo", menuName = "NPB-s/Special Ammo")]
public class SpecialAmmo : NewProgressBonus
{
    [SerializeField] private SpecificType type;

    public override void Apply()
    {
        PlayerUpgrades.Instance.ShootingUpgrades.AddSpecific(type);
    }

    public override void Cancel()
    {
        PlayerUpgrades.Instance.ShootingUpgrades.RemoveSpecific(type);
    }
}
