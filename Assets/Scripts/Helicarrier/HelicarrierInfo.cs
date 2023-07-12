using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicarrierInfo : Info
{
    [Header("Info: ")]
    [SerializeField] private float health;

    public override float InputMaxHealth => health;
    public override float InputInteractMod => 0f;

    void Start()
    {
        Heal(999f);
        DetectorPool.Instance.AddInPool(transform, DetectType.Building);
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
}
