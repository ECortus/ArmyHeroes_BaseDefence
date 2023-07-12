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
    public override float InputInteractMod => damage;

    public override void Resurrect()
    {
        Died = false;
        
        base.Resurrect();
        Heal(MaxHealth);
    }

    public override void Death()
    {
        Died = true;
        controller.Death();

        base.Death();
    }

    public override void Interact(Info nf)
    {
        if(nf != null) nf.GetHit(InteractMod);
    }
}
