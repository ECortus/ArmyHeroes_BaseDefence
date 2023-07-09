using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoUpgrades : MonoBehaviour
{
    float _health = 0f, _damage = 0f;

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

    public float PlusDamage
    {
        get
        {
            return _damage;
        }
        set
        {
            float val = value;
            val = Mathf.Clamp(val, 0f, MaxPercent);
            _damage = val;
        }
    }

    float MaxPercent = 100f;

    public void UpHealth(float perc)
    {
        PlusHealth += perc;
    }

    public void UpDamage(float perc)
    {
        PlusDamage += perc;
    }

    public void DownHealth(float perc)
    {
        PlusHealth -= perc;
    }

    public void DownDamage(float perc)
    {
        PlusDamage -= perc;
    }
}
