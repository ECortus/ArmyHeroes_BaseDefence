using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WaveRewardLVLs
{
    public readonly static float WCRPercentUP = 0.05f;

    public static int LVL
    {
        get
        {
            return PlayerPrefs.GetInt("WCRLVL_UP_permanent_bonus", 0);
        }
        set
        {
            PlayerPrefs.SetInt("WCRLVL_UP_permanent_bonus", value);
            PlayerPrefs.Save();
        }
    }
    public static void UpWCR() => LVL++;
    public static float WCRMod => LVL * WCRPercentUP;
}
