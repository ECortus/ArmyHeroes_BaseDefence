using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engineer : HumanoidController
{
    public static readonly int _Speed = Animator.StringToHash("Speed");
    public static readonly int _Repair = Animator.StringToHash("Repair");
    public static readonly int _Death = Animator.StringToHash("Death");

    [SerializeField] private EngineerDetector ed;

    protected override void UpdateAnimator()
    {
        if(Animator == null) return;
        
        Animator.SetFloat(_Speed, Agent.velocity.magnitude);
        Animator.SetBool(_Repair, ed.Repairing);
        Animator.SetBool(_Death, Died);
    }
}
