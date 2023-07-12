using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikemanInfo : Info
{
    [Header("Info: ")]
    [SerializeField] private float health;
    [SerializeField] private Pikeman pikeman;
    public float miningForce = 2f;

    public override float InputMaxHealth => health;
    public override float InputInteractMod => miningForce;

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
        
        pikeman.Death();
        base.Death();
    }

    public override void Interact(Info nf = null)
    {
        Crystal.Plus((int)InteractMod);
    }
}
