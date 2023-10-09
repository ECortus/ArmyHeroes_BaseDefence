using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BuildingUpgradesLVLs
{
    public readonly static float HPPercentUP = 0.05f;

    public static int HPLVL
    {
        get
        {
            return PlayerPrefs.GetInt("BuildingLVL_UP_permanent_bonus", 0);
        }
        set
        {
            PlayerPrefs.SetInt("BuildingLVL_UP_permanent_bonus", value);
            PlayerPrefs.Save();
        }
    }
    public static void UpHP() => HPLVL++;
    public static float HPMod => HPLVL * HPPercentUP;

    public readonly static float CostPercentDown = 0.05f;

    public static int CostLVL
    {
        get
        {
            return PlayerPrefs.GetInt("BuildingLVL_UP_permanent_bonus", 0);
        }
        set
        {
            PlayerPrefs.SetInt("BuildingLVL_UP_permanent_bonus", value);
            PlayerPrefs.Save();
        }
    }
    public static void DownCost() => CostLVL++;
    public static float CostMod => Mathf.Pow(1f - CostPercentDown, CostLVL);
}
