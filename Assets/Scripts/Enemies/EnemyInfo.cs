using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : Info
{
    [Header("Info: ")]
    [SerializeField] private float health;
    [SerializeField] private float damage;
    public HumanoidController controller;

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
        controller.Died = true;
        transform.parent.gameObject.SetActive(false);

        base.Death();
    }
}
