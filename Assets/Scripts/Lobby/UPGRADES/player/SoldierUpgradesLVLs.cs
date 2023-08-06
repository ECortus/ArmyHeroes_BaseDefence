using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoldierUpgradesLVLs
{
    public readonly static float DMGPercentUP = 0.05f;
    public readonly static float HPPercentUP = 0.05f;

    public static int LVL
    {
        get
        {
            return PlayerPrefs.GetInt("SoldierLVL_UP_permanent_bonus", 0);
        }
        set
        {
            PlayerPrefs.SetInt("SoldierLVL_UP_permanent_bonus", value);
            PlayerPrefs.Save();
        }
    }
    public static void UpHP() => LVL++;
    public static float HPMod => LVL * HPPercentUP * 100f;

    public static void UpDMG() => LVL++;
    public static float DMGMod => LVL * DMGPercentUP * 100f;
}
