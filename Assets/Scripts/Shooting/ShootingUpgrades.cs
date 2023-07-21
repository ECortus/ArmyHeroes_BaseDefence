using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingUpgrades : MonoBehaviour
{
    public SpecificAmmoEffect specificAmmoUp { get; set; }
    public void SetSAE(SpecificAmmoEffect eff)
    {
        specificAmmoUp = eff;
    }
    public void ResetSAE()
    {
        specificAmmoUp = null;
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
