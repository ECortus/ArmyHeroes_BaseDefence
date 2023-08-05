using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPB_UpDamage", menuName = "NPB-s/Up Damage")]
public class UpDamage : NewProgressBonus
{
    public float DefaultMod
    {
        get
        {
            float amount = 0f;

            if(DefaultUsedCount > 0) amount += 15f;

            if(DefaultUsedCount > 1)
            {
                for(int i = 0; i < DefaultUsedCount - 1; i++)
                {   
                    amount += 10f;
                }
            }

            return amount;
        }
    }

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
