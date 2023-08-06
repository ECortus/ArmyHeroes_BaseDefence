using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_DMG_SPD : MonoBehaviour
{
    [HideInInspector] public float _bHP = 0f;
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

    [HideInInspector] public float _bDMG = 0f;
    public virtual float bonusDMG
    {
        get
        {
            return _bDMG;
        }
        set
        {
            _bDMG = value;
        }
    }
    public void AddDMGPercent(float perc)
    {
        bonusDMG += perc;
    }

    public void ResetDMG()
    {
        bonusDMG = 0f;
    }

    [HideInInspector] public float _bSPD = 0f;
    public virtual float bonusSPD
    {
        get
        {
            return _bSPD;
        }
        set
        {
            _bSPD = value;
        }
    }
    public void AddSPDPercent(float perc)
    {
        bonusSPD += perc;
    }

    public void ResetSPD()
    {
        bonusSPD = 0f;
    }
}
