using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MechController : Target
{
    [SerializeField] private NavMeshAgent Agent;
    [SerializeField] private Fireposition fireposition;

    [Space]
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float minDistanceToTarget;
    [SerializeField] private Transform[] Muzzles;

    Vector3 dir;
    Quaternion targetRotation;

    int motor = 0;
    public void SetMotor(int mtr)
    {
        motor = mtr;
    }

    void Update()
    {
        if(fireposition.Busy)
        {
            if(motor > 0 && Vector3.Distance(target.position, transform.position) > minDistanceToTarget)
            {
                Agent.enabled = true;
                Move();
            }
            else
            {
                Agent.enabled = false;
            }

            CorrectMuzzleRotation();
            Rotate();
        }
    }

    void Move()
    {
        if(Agent.destination != target.position)
        {
            Agent.SetDestination(target.position);
        }
    }

    void Rotate()
    {
        if(target == transform)
        {
            dir = transform.forward;
        }
        else
        {
            dir = (target.position - transform.position).normalized;
            dir.y = 0f;
        }

        targetRotation = Quaternion.LookRotation(dir);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation, targetRotation, Time.deltaTime * rotateSpeed
        );
    }

    Quaternion muzzleRotation;
    Vector3 muzzleAngles;

    void CorrectMuzzleRotation()
    {
        if(target != transform)
        {
            foreach(Transform muzzle in Muzzles)
            {
                muzzleRotation = Quaternion.LookRotation((target.position + new Vector3(0f, 0.5f, 0f) - muzzle.position));
                muzzle.rotation = muzzleRotation;
            }
        }
    }
}
