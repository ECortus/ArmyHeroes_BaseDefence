using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPB_UpSpeed", menuName = "NPB-s/Up Speed")]
public class UpSpeed : NewProgressBonus
{
    public float DefaultMod
    {
        get
        {
            float amount = 0f;

            if(DefaultUsedCount > 0)
            {
                for(int i = 0; i < DefaultUsedCount; i++)
                {   
                    amount += 10f;
                }
            }

            return amount;
        }
    }

    public override void Apply()
    {
        PlayerUpgrades.Instance.AddSPDPercent(10f);
    }

    public override void Cancel()
    {
        PlayerUpgrades.Instance.ResetSPD();
    }
}
