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

    [HideInInspector] public float DecreaseSC = 0f;
    public void AddDecreaseSC(float sc)
    {
        DecreaseSC += sc;
    }
    public void ResetDecreaseSC()
    {
        DecreaseSC = 0f;
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
