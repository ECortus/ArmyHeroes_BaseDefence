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
    [SerializeField] private int costDownPerLvl = 50;

    public int Cost
    {
        get
        {
            return defCost + costDownPerLvl * CostLVL;
        }
    }

    private int CostLVL
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
    [SerializeField] private float durationUpPerLvl = 50;

    public float Duration
    {
        get
        {
            return defDuration + durationUpPerLvl * DurationLVL;
        }
    }

    private int DurationLVL
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
