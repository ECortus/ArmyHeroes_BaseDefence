using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanoidController : Target
{
    public Animator Animator;
    public NavMeshAgent Agent;
    public Rigidbody rb;
    public Detection detection;
    
    public bool Active => detection.Active;
    public bool Died => detection.Died;

    public virtual void On(Vector3 pos, Quaternion rot = new Quaternion())
    {
        Agent.enabled = true;

        TeleportToPoint(pos);
        transform.rotation = rot;

        gameObject.SetActive(true);

        detection.Resurrect();
        detection.Pool();

        takeControl = false;
    }

    public virtual void Death()
    { 
        /* detection.Depool(); */

        ResetTarget();
        takeControl = true;

        Agent.enabled = false;
    }

    public virtual void Off() 
    { 
        gameObject.SetActive(false);
        detection.Depool();
    }

    void Update()
    {
        if(Active)
        {
            ZeroRBVelocities();

            if(!takeControl) 
            {
                Move();
            }
            
            UpdateAnimator();
            Rotate();
        }
        else
        {
            ZeroAgentVelocity();
            UpdateAnimator();
        }
    }

    protected virtual void UpdateAnimator() 
    { 

    }

    private void Move()
    {
        if(Agent.isActiveAndEnabled && target != transform)
        {
            SetDestination(target.position);
        }
    }

    public void SetDestination(Vector3 point)
    {
        if(Agent.destination != point)
        {
            Agent.SetDestination(point);
        }
    }

    public bool NearPoint(Vector3 point, float distance)
    {
        return Vector3.Distance(transform.position, point) <= distance;
    }

    private void Rotate()
    {
        Vector3 dir = Vector3.zero;
        if(target != transform) dir = DirectionToTarget;
        else dir = transform.forward;

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

    void ZeroAgentVelocity()
    {
        Agent.velocity = Vector3.zero;
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
