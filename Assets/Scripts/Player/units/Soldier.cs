using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : HumanoidController
{
    public static readonly int _Speed = Animator.StringToHash("Speed");
    public static readonly int _Shooting = Animator.StringToHash("Shooting");
    public static readonly int _Death = Animator.StringToHash("Death");

    [SerializeField] private Shooting shooting;
    
    protected override void UpdateAnimator()
    {
        if(Animator == null) return;
        
        Animator.SetFloat(_Speed, Agent.velocity.magnitude);
        Animator.SetBool(_Shooting, shooting.isEnable);
        Animator.SetBool(_Death, Died);
    }
}
