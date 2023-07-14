using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerInfo : Info
{
    [Header("Info: ")]
    [SerializeField] private float health;
    [SerializeField] private Engineer engineer;
    public float repairForce = 2f;

    public override float InputMaxHealth => health;
    public override float InputDamage => repairForce;

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

        engineer.Death();
        base.Death();
    }

    public override void Interact(Info nf)
    {
        
    }
}
