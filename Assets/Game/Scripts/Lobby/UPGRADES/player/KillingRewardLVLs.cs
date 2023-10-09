using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KillingRewardLVLs
{
    public readonly static float EnemyKillingEXPRewardPercentUP = 0.05f;

    public static int LVL
    {
        get
        {
            return PlayerPrefs.GetInt("EnemyKillingEXPRewardLVL_UP_permanent_bonus", 0);
        }
        set
        {
            PlayerPrefs.SetInt("EnemyKillingEXPRewardLVL_UP_permanent_bonus", value);
            PlayerPrefs.Save();
        }
    }
    public static void UpEnemyKillingEXPReward() => LVL++;
    public static float EnemyKillingEXPRewardMod => LVL * EnemyKillingEXPRewardPercentUP;
}
