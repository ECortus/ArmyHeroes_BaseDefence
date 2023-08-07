using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorkersUpgradesLVLs
{
    public readonly static int PikemanResourcePercentUP = 1;
    public readonly static float DoctorHealTimePercentDown = 0.05f;
    public readonly static float EngineerRepairSpeedPercentUP = 0.05f;

    public static int PikemanResourceLVL
    {
        get
        {
            return PlayerPrefs.GetInt("PikemanResourceLVL_UP_permanent_bonus", 0);
        }
        set
        {
            PlayerPrefs.SetInt("PikemanResourceLVL_UP_permanent_bonus", value);
            PlayerPrefs.Save();
        }
    }
    public static void UpPikemanResource() => PikemanResourceLVL++;
    public static int PikemanResourceMod => PikemanResourceLVL * PikemanResourcePercentUP;

    public static int DoctorHealTimeLVL
    {
        get
        {
            return PlayerPrefs.GetInt("DoctorHealTimeLVL_UP_permanent_bonus", 0);
        }
        set
        {
            PlayerPrefs.SetInt("DoctorHealTimeLVL_UP_permanent_bonus", value);
            PlayerPrefs.Save();
        }
    }
    public static void UpDoctorHealTime() => DoctorHealTimeLVL++;
    public static float DoctorHealTimeMod => Mathf.Pow(1f - DoctorHealTimePercentDown, DoctorHealTimeLVL);

    public static int EngineerRepairSpeedLVL
    {
        get
        {
            return PlayerPrefs.GetInt("EngineerRepairSpeedLVL_UP_permanent_bonus", 0);
        }
        set
        {
            PlayerPrefs.SetInt("EngineerRepairSpeedLVL_UP_permanent_bonus", value);
            PlayerPrefs.Save();
        }
    }
    public static void UpEngineerRepairSpeed() => EngineerRepairSpeedLVL++;
    public static float EngineerRepairSpeedMod => Mathf.Pow(1f - EngineerRepairSpeedPercentUP, EngineerRepairSpeedLVL);
}
