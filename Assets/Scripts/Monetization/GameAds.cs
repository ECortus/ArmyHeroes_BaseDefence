using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public static class GameAds
{
    public static bool NoAds => NoAdsObjectBuyed || Subcribed;

    public static bool NoAdsObjectBuyed
    {
        get
        {
            return PlayerPrefs.GetInt("NoAds", 0) != 0;
        }
        set
        {
            PlayerPrefs.SetInt("NoAds", value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static void ActivateNoAds()
    {
        NoAdsObjectBuyed = true;
    }
    
    public static bool Subcribed 
    {
        get
        {
            return PlayerPrefs.GetInt("NoAds", 0) != 0;
        }
        set
        {
            PlayerPrefs.SetInt("NoAds", value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static void ActivateSub()
    {
        Subcribed = true;
    }
    
    public static void DeactivateSub()
    {
        Subcribed = false;
    }
}
