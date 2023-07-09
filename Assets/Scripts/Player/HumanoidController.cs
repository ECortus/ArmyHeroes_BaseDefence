using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanoidController : MonoBehaviour
{
    public static readonly int _Speed = Animator.StringToHash("Speed");

    public Animator Animator;
    public NavMeshAgent Agent;
    public Rigidbody rb;

    public bool Died { get; set; }
    public bool takeControl { get; set; }

    private Transform _target;
    public Transform target
    {
        get
        {
            if(_target == null) return transform;
            else return _target;
        }
        set
        {
            _target = value;
        }
    }

    public Vector3 DirectionToTarget
    {
        get
        {
            if(target == null) return Vector3.zero;

            Vector3 dir = (target.position - transform.position).normalized;
            dir.y = 0f;
            return dir;
        }
    }

    public void SetTarget(Transform trg)
    {
        target = trg;
    }

    public void ResetTarget()
    {
        target = null;
    }

    [HideInInspector] public Vector3 direction;
    public bool Active = true;

    public virtual void On() { }
    public virtual void Off() { }

    private void FixedUpdate()
    {
        if(Active)
        {
            UpdateDirection();
            UpdateAnimator();

            if(!takeControl) Move();
            
            Rotate();
        }
        else
        {
            ZeroAgentVelocityAndDirection();
            UpdateAnimator();
        }
    }

    protected virtual void UpdateDirection() { }
    protected virtual void UpdateAnimator() { }

    private void Move()
    {
        ZeroRBVelocities();

        if (Agent.isActiveAndEnabled && direction != Vector3.zero)
        {
            Agent.Move(direction * (Agent.speed * Time.deltaTime));
        }
    }

    private void Rotate()
    {
        Vector3 dir = Vector3.zero;
        if(target != transform) dir = DirectionToTarget;
        else dir = direction;

        if (Agent.isActiveAndEnabled && dir != Vector3.zero)
        {
            var targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * Agent.angularSpeed);
        }
    }

    void ZeroRBVelocities()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void ZeroAgentVelocityAndDirection()
    {
        Agent.velocity = Vector3.zero;
        direction = Vector3.zero;
    }

    public void TeleportToPoint(Vector3 point)
    {
        int mask = LayerMask.NameToLayer("NavMesh");
        NavMesh.SamplePosition(point, out var hit, 5f, mask);

        if (hit.hit)
        {
            Agent.enabled = false;
            transform.position = point;
            Agent.enabled = true;
        }
    }
}
