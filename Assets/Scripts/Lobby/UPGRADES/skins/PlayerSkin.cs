using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerSkin
{
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

    public static float HPMod => 0f;
    public static float DMGMod => 0f;
}
