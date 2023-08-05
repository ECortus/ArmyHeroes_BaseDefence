using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPB_UpgradeSoldiers", menuName = "NPB-s/Upgrade Soldiers")]
public class UpgradeSoldiers : NewProgressBonus
{
    private Detection[] soldiers => DetectionPool.Instance.RequirePools(DetectType.Soldier);
    private Detection soldier;

    public override void Apply()
    {
        foreach(Detection sldr in soldiers)
        {
            soldier = sldr;
            if(soldier != null)
            {
                if(ApplyCount == 0)
                {
                    soldier.HPvDMGvSPD.AddHPPercent(20f);
                    soldier.HPvDMGvSPD.AddDMGPercent(50f);
                }
                else
                {
                    soldier.HPvDMGvSPD.AddHPPercent(50f);
                    soldier.HPvDMGvSPD.AddDMGPercent(10f);
                }
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