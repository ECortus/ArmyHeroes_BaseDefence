using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPB_DecreaseShootingCooldown", menuName = "NPB-s/Decrease Shooting Cooldown")]
public class DecreaseShootingCooldown : NewProgressBonus
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
        PlayerUpgrades.Instance.ShootingUpgrades.AddDecreaseSC(10f);
    }

    public override void Cancel()
    {
        PlayerUpgrades.Instance.ShootingUpgrades.ResetDecreaseSC();
    }
}
