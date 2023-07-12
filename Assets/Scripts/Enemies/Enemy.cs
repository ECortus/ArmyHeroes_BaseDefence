using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy : HumanoidController
{
    public static readonly int _Speed = Animator.StringToHash("Speed");
    public static readonly int _MeleeAttack = Animator.StringToHash("MeleeAttack");

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

    public void Attack(Info nf)
    {
        info.Interact(nf);
    }

    public override void On(Vector3 pos, Quaternion rot = new Quaternion())
    {
        info.Resurrect();

        transform.position = pos;
        transform.rotation = rot;

        SetTarget(MainTarget);
        gameObject.SetActive(true);
        base.On();
    }

    public override void Death()
    {
        gameObject.SetActive(false);

        base.Death();
    }

    public override void Off()
    {
        gameObject.SetActive(false);

        base.Off();
    }

    protected override void Update()
    {
        /* if(AdditionalTarget != MainTarget && AdditionalTarget != null)
        {
            if((MainTarget.position - transform.position).magnitude > 15f)
            {
                SetTarget(MainTarget);
            }
            else
            {
                SetTarget(AdditionalTarget);
            }
        } */

        base.Update();
    }

    protected override void Move()
    {
        MoveByDestination();
    }

    protected override void UpdateDirection()
    {
        direction = (target.position - transform.position).normalized;
        direction.y = 0f;
    }

    protected override void UpdateAnimator()
    {
        if(Animator == null) return;
        
        Animator.SetFloat(_Speed, direction.magnitude);
        Animator.SetBool(_MeleeAttack, melee.Fighting);
    }
}
