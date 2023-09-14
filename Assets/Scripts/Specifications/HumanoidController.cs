using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cysharp.Threading.Tasks;

public class HumanoidController : Target
{
    [Space]
    [SerializeField] private float defaultSpeed;
    private float speed
    {
        get
        {
            if(detection.HPvDMGvSPD == null)
            {
                return defaultSpeed;
            }
            
            return defaultSpeed * (1f + detection.HPvDMGvSPD.bonusSPD / 100f);
        }
    }

    [Space]
    public Animator Animator;
    public NavMeshAgent Agent;
    public Rigidbody rb;
    public Detection detection;

    public Vector3 Center
    {
        get
        {
            return transform.position + new Vector3(0f, 0.5f, 0f);
        }
    }
    
    public bool Active => detection.Active;

    public bool Died => detection.Died ;
    public virtual void On(Vector3 pos, Quaternion rot = new Quaternion())
    {
        Agent.enabled = true;

        TeleportToPoint(pos);
        transform.rotation = rot;

        gameObject.SetActive(true);

        detection.Pool();
        detection.Resurrect();

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
        if(!Died && Active && GameManager.Instance.isActive)
        {
            if(Agent.isActiveAndEnabled && Agent.isOnNavMesh)
            {
                Agent.speed = speed;
                ZeroRBVelocities();

                if(!takeControl) 
                {
                    Agent.isStopped = false;
                    Move();
                }
                else
                {
                    Agent.isStopped = true;
                }
            }
            
            UpdateAnimator();
            Rotate();
        }
        else
        {
            ZeroRBVelocities();

            ZeroAgentVelocity();
            UpdateAnimator();
        }
    }

    protected virtual void UpdateAnimator() 
    { 

    }

    private void Move()
    {
        if(target != transform)
        {
            SetDestination(target.position);
        }
    }

    public void SetDestination(Vector3 point)
    {
        /*if(Agent.destination != point)*/
        {
            Agent.SetDestination(point);
        }
    }

    public void ResetDestination()
    {
        if(Agent.isActiveAndEnabled) SetDestination(transform.position);
    }

    public bool NearPoint(Vector3 point, float distance)
    {
        return Vector3.Distance(Center, point) <= distance;
    }

    protected virtual void Rotate()
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
