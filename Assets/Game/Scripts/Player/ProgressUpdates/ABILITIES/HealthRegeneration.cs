using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPB_HealthRegeneration", menuName = "NPB-s/Health Regeneration")]
public class HealthRegeneration : NewProgressBonus
{
    public override void Apply()
    {
        PlayerInfo.Instance.AddRegenPercent(0.05f);
    }

    public override void Cancel()
    {
        PlayerInfo.Instance.ResetAutoRegeneration();
    }
}
