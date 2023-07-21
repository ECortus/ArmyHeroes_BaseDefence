using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Weapon : Shooting
{
    [SerializeField] private Detection detection;

    [SerializeField] private List<WeaponTuple> List = new List<WeaponTuple>();

    public int Index { get; set; }
    private WeaponInfo info => List[Index].Info;

    public void SetNewWeapon(int i)
    {
        bool shooted = isEnable;
        
        foreach(WeaponTuple tuple in List)
        {
            tuple.Model.SetActive(false);
        }
        Disable();

        Index = i % List.Count;
        List[i].Model.SetActive(true);

        if(shooted)
        {
            delaypoolvar = 0f;
            Enable();
        }
    }

    void Start()
    {
        Disable();
    }

    public override void Enable()
    {
        base.Enable();
        /* List[Index].Model.SetActive(true); */
    }

    public override void Disable()
    {
        base.Disable();
        /* List[Index].Model.SetActive(false); */
    }

    public override float Damage
    {
        get
        {
            if(detection.HPvDMGvSPD == null)
            {
                return info.Damage;
            }

            return info.Damage * (1f + detection.HPvDMGvSPD.bonusDMG / 100f);
        }
    }
    protected override float Delay
    {
        get
        {
            if(ups == null)
            {
                return info.DelayPerShot;
            }

            return info.DelayPerShot * (1f - ups.DecreaseSC / 100f);
        }
    }
    protected override List<Transform> Muzzles => info.Muzzles;
    protected override Ammo Ammo => info.Ammo;
}

[System.Serializable]
public class WeaponTuple
{
    public GameObject Model;
    public WeaponInfo Info;
}

[System.Serializable]
public class WeaponInfo
{
    public float Damage = 1f;
    public float DelayPerShot = 0.5f;
    public List<Transform> Muzzles = new List<Transform>();
    public Ammo Ammo;
}
