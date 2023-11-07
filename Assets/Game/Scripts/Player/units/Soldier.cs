using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : HumanoidController
{
    public static readonly int _Speed = Animator.StringToHash("Speed");
    public static readonly int _Shooting = Animator.StringToHash("Shooting");
    public static readonly int _Death = Animator.StringToHash("Death");

    [SerializeField] private GunHandler shooting;
    [SerializeField] private Transform weapon;

    void Start()
    {
        shooting.SetGunPair(0);
    }
    
    protected override void UpdateAnimator()
    {
        if(Animator == null) return;
        
        Animator.SetFloat(_Speed, Agent.velocity.magnitude);
        Animator.SetBool(_Shooting, shooting.isEnable);
        Animator.SetBool(_Death, Died);
    }
    
    protected override void Rotate()
    {
        Vector3 dir;

        if(Agent.velocity.magnitude < 0.05f)
        {
            dir = (target.position - transform.position).normalized;
        }
        else
        {
            dir = Agent.velocity.normalized;
        }

        if (Agent.isActiveAndEnabled && dir != Vector3.zero)
        {
            dir.y = 0f;
            
            var targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * Agent.angularSpeed);

            dir = (target.position - weapon.position).normalized;
            dir.y = 0f;
            
            targetRotation = Quaternion.LookRotation(dir);
            weapon.rotation = Quaternion.RotateTowards(weapon.rotation, targetRotation, Time.deltaTime * 135f);
        }
    }
}
