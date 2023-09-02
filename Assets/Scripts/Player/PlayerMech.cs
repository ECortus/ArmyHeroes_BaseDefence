using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMech : Target
{
    [SerializeField] private NavMeshAgent Agent;
    [SerializeField] private Detection detection;

    [Space]
    [SerializeField] private Animation walk;

    [Space]
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Transform[] Muzzles;
    
    private FloatingJoystick joyStick => GameManager.Instance.Joystick;

    Vector3 dir, Direction;
    Quaternion targetRotation;
    
    public void On(Vector3 pos)
    {
        gameObject.SetActive(true);
        TeleportToPoint(pos);
        
        detection.Resurrect();
    }

    public void Off() 
    {
        detection.Death();
        
        gameObject.SetActive(false);
    }

    void Update()
    {
        
        {
            UpdateDirection();
            Move();

            CorrectMuzzleRotation();
            Rotate();
        }

        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        if((Direction == Vector3.zero && (transform.eulerAngles - targetRotation.eulerAngles).magnitude < 0.05f) || !Agent.isActiveAndEnabled)
        {
            walk.Stop();
        }

        if((Direction != Vector3.zero || (transform.eulerAngles - targetRotation.eulerAngles).magnitude > 0.05f) 
            && !walk.isPlaying)
        {
            walk.Play();
        }
    }

    void Move()
    {
        if(Direction != Vector3.zero)
        {
            Agent.Move(Direction * (Agent.speed * Time.deltaTime));
        }
    }
    
    void UpdateDirection()
    {
        if(joyStick.gameObject.activeSelf)
        {
            Direction = new Vector3(joyStick.Horizontal, 0, joyStick.Vertical).normalized;
            Direction = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up) * Direction;
        }
        else
        {
            Direction = Vector3.zero;
        }
    }

    void Rotate()
    {
        if(target == transform)
        {
            if (Direction != Vector3.zero) dir = Direction;
            else dir = transform.forward;
        }
        else
        {
            dir = (target.position + new Vector3(0f, 1f, 0f) - transform.position).normalized;
            dir.y = 0f;
        }

        targetRotation = Quaternion.LookRotation(dir);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation, targetRotation, Time.deltaTime * rotateSpeed
        );
    }

    Quaternion muzzleRotation;

    void CorrectMuzzleRotation()
    {
        if(target != transform)
        {
            foreach(Transform muzzle in Muzzles)
            {
                muzzleRotation = Quaternion.LookRotation(
                    (target.position + new Vector3(0f, 1f, 0f) - muzzle.position).normalized
                );
                muzzle.rotation = muzzleRotation;
            }
        }
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
