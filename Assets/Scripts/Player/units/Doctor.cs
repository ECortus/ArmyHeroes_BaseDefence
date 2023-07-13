using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doctor : HumanoidController
{
    public static readonly int _Speed = Animator.StringToHash("Speed");

    public override void On(Vector3 pos = new Vector3(), Quaternion rot = new Quaternion())
    {
        transform.position = pos;
        transform.rotation = rot;

        gameObject.SetActive(true);
        base.On();
    }

    public void Heal(Info nf)
    {
        info.Interact(nf);
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

    protected override void Move()
    {
        MoveByDestination(target);
    }

    protected override void UpdateAnimator()
    {
        if(Animator == null) return;
        
        Animator.SetFloat(_Speed, direction.magnitude);
    }
}
