using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ulta : ScriptableObject
{
    public int PowerLVL
    {
        get
        {
            return PlayerPrefs.GetInt(this.name + "_UltaPowerUp", 0);
        }
        set
        {
            PlayerPrefs.SetInt(this.name + "_UltaPowerUp", value);
            PlayerPrefs.Save();
        }
    }

    public void UpPowerLVL()
    {
        PowerLVL++;
    }

    [SerializeField] private int defCost = 100;
    [Range(0f, 99f)]
    public int costDownPerLvlOnPercent = 25;

    public int Cost
    {
        get
        {
            return (int)(defCost * CostMod);
        }
    }

    public float CostMod
    {
        get
        {
            float down = costDownPerLvlOnPercent;
            return Mathf.Pow(1f - down / 100f, CostLVL);
        }
    }

    public int CostLVL
    {
        get
        {
            return PlayerPrefs.GetInt(this.name + "_UltaCostDown", 0);
        }
        set
        {
            PlayerPrefs.SetInt(this.name + "_UltaCostDown", value);
            PlayerPrefs.Save();
        }
    }

    public void DecreaseCost()
    {
        CostLVL++;
    }
 
    [Space]
    [SerializeField] private float defDuration = 15f;
    public float durationUpPerLvl = 50;

    public float Duration
    {
        get
        {
            return defDuration + DurationMod;
        }
    }

    public float DurationMod
    {
        get
        {
            return durationUpPerLvl * DurationLVL;
        }
    }

    public int DurationLVL
    {
        get
        {
            return PlayerPrefs.GetInt(this.name + "_UltaDurationUp", 0);
        }
        set
        {
            PlayerPrefs.SetInt(this.name + "_UltaDurationUp", value);
            PlayerPrefs.Save();
        }
    }

    public void IncreaseDuration()
    {
        DurationLVL++;
    }   

    public virtual void Activate()
    {
        
    }

    public virtual void Deactivate()
    {

    }
}
