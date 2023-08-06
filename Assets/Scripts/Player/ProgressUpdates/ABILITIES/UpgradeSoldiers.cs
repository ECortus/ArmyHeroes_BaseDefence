using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPB_UpgradeSoldiers", menuName = "NPB-s/Upgrade Soldiers")]
public class UpgradeSoldiers : NewProgressBonus
{
    private Detection[] soldiers => DetectionPool.Instance.RequirePools(DetectType.Soldier);
    private Detection soldier;

    public float PlusHP
    {
        get
        {
            float bonus = 0f;

            for(int i = 0; i < UsedCount; i++)
            {
                if(i == 0) bonus += 20f;
                else bonus += 50f;
            }

            return bonus;
        }
    }

    public float PlusDMG
    {
        get
        {
            float bonus = 0f;

            for(int i = 0; i < UsedCount; i++)
            {
                if(i == 0) bonus += 50f;
                else bonus += 10f;
            }

            return bonus;
        }
    }

    public override void Apply()
    {
        foreach(Detection sldr in soldiers)
        {
            soldier = sldr;
            if(soldier != null)
            {
                soldier.HPvDMGvSPD.ResetHP();
                soldier.HPvDMGvSPD.ResetDMG();

                soldier.HPvDMGvSPD.AddHPPercent(PlusHP);
                soldier.HPvDMGvSPD.AddDMGPercent(PlusDMG);
            }
        }
    }

    public override void Cancel()
    {
        foreach(Detection sldr in soldiers)
        {
            soldier = sldr;
            if(soldier != null)
            {
                soldier.HPvDMGvSPD.ResetHP();
                soldier.HPvDMGvSPD.ResetDMG();
            }
        }
    }
}