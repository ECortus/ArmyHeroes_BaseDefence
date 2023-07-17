using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy : HumanoidController
{
    public static readonly int _Speed = Animator.StringToHash("Speed");
    public static readonly int _MeleeAttack = Animator.StringToHash("MeleeAttack");
    public static readonly int _Death = Animator.StringToHash("Death");

    public EnemyType Type = EnemyType.Default;
    [HideInInspector] public Transform MainTarget, AdditionalTarget;
    
    [SerializeField] private MeleeAttacker melee;

    public void SetMainTarget(Transform mt)
    {
        MainTarget = mt;
        SetTarget(MainTarget);
    }

    public void SetAdditionalTarget(Transform mt)
    {
        AdditionalTarget = mt;
        SetTarget(AdditionalTarget);
    }

    public override void On(Vector3 pos, Quaternion rot = new Quaternion())
    {
        base.On(pos);
        SetTarget(MainTarget);
    }

    protected override void UpdateAnimator()
    {
        if(Animator == null) return;
        
        Animator.SetFloat(_Speed, Agent.velocity.magnitude);
        Animator.SetBool(_MeleeAttack, melee.Fighting);
        Animator.SetBool(_Death, Died);
    }
}
