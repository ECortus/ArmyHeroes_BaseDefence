using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cysharp.Threading.Tasks;

public class Player : Target
{
    public static Player Instance { get; set; }
    void Awake() => Instance = this;

    public Transform Transform { get { return transform; } }

    public static readonly int _Speed = Animator.StringToHash("Speed");
    public static readonly int _Shooting = Animator.StringToHash("Shooting");
    public static readonly int _Death = Animator.StringToHash("Death");

    private FloatingJoystick joyStick => GameManager.Instance.Joystick;

    [SerializeField] private Animator Animator;
    [SerializeField] private NavMeshAgent Agent;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Shooting shooting;
    [SerializeField] private BaseDetector detector;
    [SerializeField] private Detection detection;

    [HideInInspector] public Vector3 Direction;
    
    public bool Active => detection.Active;
    public bool Died => detection.Died;

    void Start()
    {
        On();
    }

    public void On(Vector3 pos = new Vector3(), Quaternion rot = new Quaternion())
    {
        detection.Resurrect();

        TeleportToPoint(pos);
        transform.rotation = rot;

        gameObject.SetActive(true);
        detection.Pool();
        detector.On();
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
    }

    void Update()
    {
        if(Active)
        {
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
        Animator.SetBool(_Shooting, shooting.isEnable && !Died);
        Animator.SetBool(_Death, Died);
    }

    void Move()
    {
        if(target != null && takeControl)
        {
            if(Agent.destination != target.position) Agent.SetDestination(target.position);
            return;
        }

        if(Agent.isActiveAndEnabled && Direction != Vector3.zero)
        {
            Agent.Move(Direction * (Agent.speed * Time.deltaTime));
        }
    }

    private void Rotate()
    {
        Vector3 dir = Vector3.zero;
        if(target != transform) dir = DirectionToTarget;
        else dir = Direction;

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
