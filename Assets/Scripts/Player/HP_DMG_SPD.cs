using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_DMG_SPD : MonoBehaviour
{
    public float bonusHP { get; set; }
    public void AddHPPercent(float perc)
    {
        bonusHP += perc;
    }

    public void ResetHP()
    {
        bonusHP = 0f;
    }

    public float bonusDMG { get; set; }
    public void AddDMGPercent(float perc)
    {
        bonusDMG += perc;
    }

    public void ResetDMG()
    {
        bonusDMG = 0f;
    }

    public float bonusSPD { get; set; }
    public void AddSPDPercent(float perc)
    {
        bonusSPD += perc;
    }

    public void ResetSPD()
    {
        bonusSPD = 0f;
    }
}
