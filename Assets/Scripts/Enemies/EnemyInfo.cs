using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : Info
{
    [Header("Info: ")]
    [SerializeField] private float health;
    [SerializeField] private float damage;
    [SerializeField] private HumanoidController controller;

    public override float InputMaxHealth => health;
    public override float InputDamage => damage;

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
        Died = true;
        controller.Death();

        base.Death();
    }
}
