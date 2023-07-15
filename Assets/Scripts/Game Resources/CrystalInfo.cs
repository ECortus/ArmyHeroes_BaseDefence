using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalInfo : Info
{
    [Header("Info: ")]
    [SerializeField] private float health;

    public override float InputMaxHealth => health;
    public override float InputDamage => 0f;

    void Start()
    {
        Heal(999f);
        DetectorPool.Instance.AddInPool(this, DetectType);
    }

    public override void Heal(float mnt)
    {
        base.Heal(mnt);
    }

    public override void GetHit(float mnt)
    {
        base.GetHit(mnt);
    }

    public override void Resurrect()
    {
        base.Resurrect();
        Heal(MaxHealth);
    }

    public override void Death()
    {
        Died = true;
        base.Death();
    }

    public override void Interact(Info nf)
    {
        if(nf != null) nf.GetHit(Damage);
    }
}
