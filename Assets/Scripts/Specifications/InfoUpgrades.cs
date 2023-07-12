using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoUpgrades : MonoBehaviour
{
    float _health = 0f, _InteractMod = 0f;

    public float PlusHealth
    {
        get
        {
            return _health;
        }
        set
        {
            float val = value;
            val = Mathf.Clamp(val, 0f, MaxPercent);
            _health = val;
        }
    }

    public float PlusInteractMod
    {
        get
        {
            return _InteractMod;
        }
        set
        {
            float val = value;
            val = Mathf.Clamp(val, 0f, MaxPercent);
            _InteractMod = val;
        }
    }

    float MaxPercent = 100f;

    public void UpHealth(float perc)
    {
        PlusHealth += perc;
    }

    public void UpInteractMod(float perc)
    {
        PlusInteractMod += perc;
    }

    public void DownHealth(float perc)
    {
        PlusHealth -= perc;
    }

    public void DownInteractMod(float perc)
    {
        PlusInteractMod -= perc;
    }
}
