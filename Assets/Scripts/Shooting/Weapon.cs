using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Weapon : Shooting
{
    [SerializeField] private List<WeaponTuple> List = new List<WeaponTuple>();

    int index = 0;
    private WeaponInfo info => List[index].Info;

    public void SetNewWeapon(int i)
    {
        index = i % List.Count;
        List[i].Model.SetActive(true);
    }

    void Start()
    {
        Disable();
    }

    public override void Enable()
    {
        base.Enable();
        List[index].Model.SetActive(true);
    }

    public override void Disable()
    {
        base.Disable();
        List[index].Model.SetActive(false);
    }

    public override float Damage => info.Damage;
    protected override float Delay => info.DelayPerShot;
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
