using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pikeman : HumanoidController
{
    public override void On()
    {
        gameObject.SetActive(true);
    }

    public override void Death()
    {
        Active = false;
        gameObject.SetActive(false);
    }

    public override void Off()
    {
        gameObject.SetActive(false);
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
