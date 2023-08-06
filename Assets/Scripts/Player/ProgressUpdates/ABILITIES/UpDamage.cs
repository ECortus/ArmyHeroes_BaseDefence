using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPB_UpDamage", menuName = "NPB-s/Up Damage")]
public class UpDamage : NewProgressBonus
{
    public override void Apply()
    {
        float amount = ApplyCount == 0 ? 15f : 10f;
        PlayerUpgrades.Instance.AddDMGPercent(amount);
    }

    public override void Cancel()
    {
        PlayerUpgrades.Instance.ResetDMG();
    }
}
