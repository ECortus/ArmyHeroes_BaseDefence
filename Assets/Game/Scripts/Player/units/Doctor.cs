using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doctor : HumanoidController
{
    public static readonly int _Speed = Animator.StringToHash("Speed");
    public static readonly int _Carring = Animator.StringToHash("Carring");
    public static readonly int _Death = Animator.StringToHash("Death");

    [SerializeField] private DoctorDetector dd;

    protected override void UpdateAnimator()
    {
        if(Animator == null) return;
        
        Animator.SetFloat(_Speed, Agent.velocity.magnitude);
        Animator.SetBool(_Carring, dd.Carring);
        Animator.SetBool(_Death, Died);
    }
}
