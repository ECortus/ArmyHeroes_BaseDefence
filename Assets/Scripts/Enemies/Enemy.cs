using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy : HumanoidController
{
    [SerializeField] private Transform MainTarget;

    void Start()
    {
        On();
    }

    public override void On()
    {
        SetTarget(MainTarget);
        gameObject.SetActive(true);
    }

    public override void Off()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if(target != MainTarget)
        {
            if((MainTarget.position - transform.position).magnitude < 
                (target.position - transform.position).magnitude)
            {
                SetTarget(MainTarget);
                return;
            }
        }
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
    }
}
