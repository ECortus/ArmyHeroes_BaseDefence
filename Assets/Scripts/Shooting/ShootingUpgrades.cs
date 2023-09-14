using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingUpgrades : MonoBehaviour
{
    public SpecificType Specifics = SpecificType.Simple;
    public void AddSpecific(SpecificType type)
    {
        if(!Specifics.HasFlag(type))
        {
            Specifics |= type;
        }
    }
    public void RemoveSpecific(SpecificType type)
    {
        if(Specifics.HasFlag(type))
        {
            Specifics &= ~type;
        }
    }

    [HideInInspector] public float DecreaseSC = 1f;
    public void AddDecreaseSC(float sc)
    {
        DecreaseSC *= 1f - (sc > 1f ? sc / 100f : sc) * DecreaseSC;
    }
    public void ResetDecreaseSC()
    {
        DecreaseSC = 1f;
    }

    [HideInInspector] public int AmmoPerShotMultiple = 1;
    public void AddAmmoPerShotMultiple(int sc)
    {
        AmmoPerShotMultiple *= sc;
    }
    public void ResetAmmoPerShotMultiple()
    {
        AmmoPerShotMultiple = 1;
    }
}
