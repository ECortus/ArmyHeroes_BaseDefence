using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerSkin
{
    public static void SetSkin(SkinObject skin)
    {
        Index = skin.Index;
        HPMod = skin.HPBonus;
        DMGMod = skin.DMGBonus;
    }

    public static int Index
    {
        get
        {
            return PlayerPrefs.GetInt(DataManager.SkinKey, 0);
        }
        set
        {
            PlayerPrefs.SetInt(DataManager.SkinKey, value);
            PlayerPrefs.Save();
        }
    }

    public static float HPMod
    {
        get
        {
            return PlayerPrefs.GetFloat(DataManager.SkinKey + "HPBonus", 0f);
        }
        set
        {
            PlayerPrefs.SetFloat(DataManager.SkinKey + "HPBonus", value);
            PlayerPrefs.Save();
        }
    }

    public static float DMGMod
    {
        get
        {
            return PlayerPrefs.GetFloat(DataManager.SkinKey + "DMGBonus", 0f);
        }
        set
        {
            PlayerPrefs.SetFloat(DataManager.SkinKey + "DMGBonus", value);
            PlayerPrefs.Save();
        }
    }
}
