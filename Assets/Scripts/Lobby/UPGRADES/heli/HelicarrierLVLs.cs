using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelicarrierLVLs
{
    private static float UPHPPercentPerLvl = 5f;

    public static float HPMod
    {
        get
        {
            return HPLVL * UPHPPercentPerLvl;
        }
    }

    public static int HPLVL
    {
        get
        {
            return PlayerPrefs.GetInt("HelicarrierHPUP", 0);
        }
        set
        {
            PlayerPrefs.SetInt("HelicarrierHPUP", value);
            PlayerPrefs.Save();
        }
    }

    public static void UpHPLevel()
    {
        HPLVL++;
    }
}
