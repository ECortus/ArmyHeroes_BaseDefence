using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerUpgradesLVLs
{
    public readonly static float DMGPercentUP = 0.05f;
    public readonly static float SPDPercentUP = 0.025f;
    public readonly static float SCPercentDecrease = 0.05f;
    public readonly static float RegenPercentPerSecond = 0.01f;

    public static int DMGLVL
    {
        get
        {
            return PlayerPrefs.GetInt("DMGLVL_UP_permanent_bonus", 0);
        }
        set
        {
            PlayerPrefs.SetInt("DMGLVL_UP_permanent_bonus", value);
            PlayerPrefs.Save();
        }
    }
    public static void UpDMG() => DMGLVL++;
    public static float DMGMod => DMGLVL * DMGPercentUP * 100f;

    public static int SPDLVL
    {
        get
        {
            return PlayerPrefs.GetInt("SPDLVL_UP_permanent_bonus", 0);
        }
        set
        {
            PlayerPrefs.SetInt("SPDLVL_UP_permanent_bonus", value);
            PlayerPrefs.Save();
        }
    }
    public static void UpSPD() => SPDLVL++;
    public static float SPDMod => SPDLVL * SPDPercentUP * 100f;

    public static int SCLVL
    {
        get
        {
            return PlayerPrefs.GetInt("SCLVL_UP_permanent_bonus", 0);
        }
        set
        {
            PlayerPrefs.SetInt("SCLVL_UP_permanent_bonus", value);
            PlayerPrefs.Save();
        }
    }
    public static void DecreaseSC() => SCLVL++;
    public static float SCMod => SCLVL * SCPercentDecrease * 100f;

    public static int RegenLVL
    {
        get
        {
            return PlayerPrefs.GetInt("RegenLVL_UP_permanent_bonus", 0);
        }
        set
        {
            PlayerPrefs.SetInt("RegenLVL_UP_permanent_bonus", value);
            PlayerPrefs.Save();
        }
    }
    public static void UpRegen() => RegenLVL++;
    public static float RegenMod => RegenLVL * RegenPercentPerSecond;
}
