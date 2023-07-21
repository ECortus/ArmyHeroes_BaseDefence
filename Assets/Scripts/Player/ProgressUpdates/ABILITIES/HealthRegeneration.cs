using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPB_HealthRegeneration", menuName = "NPB-s/Health Regeneration")]
public class HealthRegeneration : NewProgressBonus
{
    public override void Apply()
    {
        float amount = UsedCount * 0.5f;
        PlayerInfo.Instance.StartAutoRegeneration(amount);
    }

    public override void Cancel()
    {
        PlayerInfo.Instance.StopAutoRegeneration();
    }
}
