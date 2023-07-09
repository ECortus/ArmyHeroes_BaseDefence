using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierInfo : Info
{
    [Header("Info: ")]
    [SerializeField] private float health;
    [SerializeField] private Weapon weapon;

    public override float InputMaxHealth => health;
    public override float InputDamage => weapon.Damage;

    void Start()
    {
        Heal(999f);
    }

    public override void Resurrect()
    {
        base.Resurrect();
        Heal(MaxHealth);
    }

    public override void Death()
    {
        base.Death();
    }
}
