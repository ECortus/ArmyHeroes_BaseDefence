using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPB_UpSpeed", menuName = "NPB-s/Up Speed")]
public class UpSpeed : NewProgressBonus
{
    public override void Apply()
    {
        PlayerUpgrades.Instance.AddSPDPercent(50f);
    }

    public override void Cancel()
    {
        PlayerUpgrades.Instance.ResetSPD();
    }
}
