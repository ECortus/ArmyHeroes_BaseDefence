using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_DMG_SPD : MonoBehaviour
{
    private float _bHP = 0f;
    public virtual float bonusHP
    {
        get
        {
            return _bHP;
        }
        set
        {
            _bHP = value;
        }
    }
    public void AddHPPercent(float perc)
    {
        bonusHP += perc;
    }

    public void ResetHP()
    {
        bonusHP = 0f;
    }

    [HideInInspector] public float bonusDMG = 0f;
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
