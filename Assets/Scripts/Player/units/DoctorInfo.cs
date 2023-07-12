using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorInfo : Info
{
    [Header("Info: ")]
    [SerializeField] private float health;
    [SerializeField] private Doctor doctor;
    public float healForce = 5f;

    public override float InputMaxHealth => health;
    public override float InputInteractMod => healForce;

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
        
        doctor.Death();
        base.Death();
    }

    public override void Interact(Info nf)
    {
        if(nf != null) nf.Heal(InteractMod);
    }
}
