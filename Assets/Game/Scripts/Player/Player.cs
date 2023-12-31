using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cysharp.Threading.Tasks;
using Zenject;

public class Player : Target
{
    [Inject] public static Player Instance { get; set; }
    [Inject] void Awake() => Instance = this;

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

    public static readonly int _Speed = Animator.StringToHash("Speed");
    public static readonly int _Death = Animator.StringToHash("Death");

    private FloatingJoystick joyStick => GameManager.Instance.Joystick;

    [SerializeField] private Animator Animator;
    [SerializeField] private NavMeshAgent Agent;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private RangeAttackPlayer detector;
    [SerializeField] private Detection detection;

    [Space]
    [SerializeField] private Transform spawnIfToHigh;

    [HideInInspector] public Vector3 Direction;
    
    public bool Active => detection.Active;
    public bool Died => detection.Died;

    void Start()
    {
        On(transform.position);
    }

    public void On(Vector3 pos = new Vector3(), Quaternion rot = new Quaternion())
    {
        detection.Resurrect();

        if(pos != new Vector3())
        {
            TeleportToPoint(pos);
            transform.rotation = rot;
        }
        
        ResetRotateDir();

        /* gameObject.SetActive(true);
        detection.Pool();
        detector.On(); */
    }

    public void Death()
    { 
        /* detection.Depool(); */
        detector.Off();
    }

    public void Off() 
    { 
        detection.Depool();
        detector.Off();

        gameObject.SetActive(false);
    }

    void Update()
    {
        if (transform.position.y > 1.5f)
        {
            TeleportToPoint(spawnIfToHigh.position);
            return;
        }
        
        if(Active && GameManager.Instance.isActive)
        {
            if(Agent.isActiveAndEnabled)
            {
                Agent.speed = speed;
                ZeroRBVelocities();

                if(!takeControl) 
                {
                    UpdateDirection();
                }
                else ZeroDirection();
                
                Move();
                UpdateAnimator();
                Rotate();
            }
        }
        else
        {
            ZeroDirection();
            UpdateAnimator();
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

    void UpdateAnimator()
    {
        Animator.SetFloat(_Speed, Direction.magnitude);
        Animator.SetBool("HaveTarget", detector.data != null || detector.addit_data != null);
        Animator.SetBool(_Death, Died);
    }

    void Move()
    {
        if(target != null && takeControl)
        {
            if(Agent.destination != target.position) Agent.SetDestination(target.position);
            return;
        }

        if(Direction != Vector3.zero)
        {
            Agent.Move(Direction * (Agent.speed * Time.deltaTime));
        }
    }

    private Vector3 rotateDir;

    public void SetRotateDir(Vector3 dir)
    {
        rotateDir = dir;
    }

    public void ResetRotateDir()
    {
        rotateDir = Vector3.zero;
    }

    private void Rotate()
    {
        Vector3 direction;

        if (rotateDir != Vector3.zero)
        {
            direction = rotateDir;
        }
        else
        {
            direction = _target != null ? DirectionToTarget : Direction;
        }
        
        if (direction != Vector3.zero)
        {
            direction.y = 0f;
            
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, 
                Quaternion.LookRotation(direction), 
                Time.deltaTime * Agent.angularSpeed
            );
        }
    }

    void ZeroRBVelocities()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void ZeroDirection()
    {
        Direction = Vector3.zero;
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

    void OnTriggerEnter(Collider col)
    {
        GameObject go = col.gameObject;

        switch(go.tag)
        {
            default:
                break;
        }
    }

    void OnTriggerExit(Collider col)
    {
        GameObject go = col.gameObject;

        switch(go.tag)
        {
            default:
                break;
        }
    }
}
