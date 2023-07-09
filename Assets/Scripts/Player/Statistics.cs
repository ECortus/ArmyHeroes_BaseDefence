using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Statistics
{
    public static int LevelIndex
    {
        get
        {
            int lvl = PlayerPrefs.GetInt(DataManager.LevelIndexKey, 0);
            return lvl;
        }

        set
        {
            int lvl = value;
            PlayerPrefs.SetInt(DataManager.LevelIndexKey, value);
            PlayerPrefs.Save();
        }
    }

    public static float Experience
    {
        get
        {
            float amount = PlayerPrefs.GetFloat(DataManager.ExperienceKey, 0);
            return amount;
        }

        set
        {
            float amount = value;
            PlayerPrefs.SetFloat(DataManager.ExperienceKey, value);
            PlayerPrefs.Save();
        }
    }

    public static int Money
    {
        get
        {
            int amount = PlayerPrefs.GetInt(DataManager.MoneyKey, 0);
            return amount;
        }

        set
        {
            int amount = value;
            PlayerPrefs.SetInt(DataManager.MoneyKey, value);
            PlayerPrefs.Save();
        }
    }

    public static int Crystal
    {
        get
        {
            int amount = PlayerPrefs.GetInt(DataManager.CrystalKey, 0);
            return amount;
        }

        set
        {
            int amount = value;
            PlayerPrefs.SetInt(DataManager.CrystalKey, value);
            PlayerPrefs.Save();
        }
    }

    public static int Token
    {
        get
        {
            int amount = PlayerPrefs.GetInt(DataManager.TokenKey, 0);
            return amount;
        }

        set
        {
            int amount = value;
            PlayerPrefs.SetInt(DataManager.TokenKey, value);
            PlayerPrefs.Save();
        }
    }
}
