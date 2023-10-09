using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pikeman : HumanoidController
{
    public static readonly int _Speed = Animator.StringToHash("Speed");
    public static readonly int _Mining = Animator.StringToHash("Mining");
    public static readonly int _Carring = Animator.StringToHash("Carring");
    public static readonly int _Death = Animator.StringToHash("Death");

    [SerializeField] private PikemanDetector pd;

    protected override void UpdateAnimator()
    {
        if(Animator == null) return;
        
        Animator.SetFloat(_Speed, Agent.velocity.magnitude);
        Animator.SetBool(_Mining, pd.Mining);
        Animator.SetBool(_Carring, pd.Carring);
        Animator.SetBool(_Death, Died);
    }
}
