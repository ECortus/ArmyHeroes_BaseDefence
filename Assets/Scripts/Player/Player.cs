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
    public static readonly int _Shooting = Animator.StringToHash("Shooting");
    public static readonly int _Death = Animator.StringToHash("Death");

    private FloatingJoystick joyStick => GameManager.Instance.Joystick;

    [SerializeField] private Animator Animator;
    [SerializeField] private NavMeshAgent Agent;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GunHandler shooting;
    [SerializeField] private BaseDetector detector;
    [SerializeField] private Detection detection;

    [HideInInspector] public Vector3 Direction;

    [Space] [SerializeField] private Transform[] Weapons;
    
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
        Animator.SetFloat(_Shooting, shooting.GunsCount);
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

    private void Rotate()
    {
        Vector3 direction = _target != null ? DirectionToTarget : Direction;
        
        if (direction != Vector3.zero)
        {
            direction.y = 0f;
            
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, 
                Quaternion.LookRotation(direction), 
                Time.deltaTime * Agent.angularSpeed
            );
            
            foreach (Transform weapon in Weapons)
            {
                if (weapon.gameObject.activeInHierarchy)
                {
                    direction = _target != null ? (target.position - weapon.position).normalized : transform.forward;
                    direction.y = 0f;
                    
                    weapon.rotation = Quaternion.RotateTowards(
                        weapon.rotation, 
                        Quaternion.LookRotation(direction), 
                        Time.deltaTime * Agent.angularSpeed * 1.25f
                    );
                }
            }
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
