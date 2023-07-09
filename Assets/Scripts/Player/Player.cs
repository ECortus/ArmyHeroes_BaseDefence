using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cysharp.Threading.Tasks;

public class Player : HumanoidController
{
    public static Player Instance { get; set; }
    public Transform Transform { get { return transform; } }

    private FloatingJoystick joyStick => GameManager.Instance.Joystick;

    private void Awake()
    {
        Instance = this;
    }

    public override void On()
    {
        gameObject.SetActive(true);
        joyStick.Reset();
    }

    public override void Off()
    {
        gameObject.SetActive(false);
    }

    protected override void UpdateDirection()
    {
        if(joyStick.gameObject.activeSelf)
        {
            direction = new Vector3(joyStick.Horizontal, 0, joyStick.Vertical).normalized;
            direction = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up) * direction;
        }
        else
        {
            direction = Vector3.zero;
        }
    }

    protected override void UpdateAnimator()
    {
        if(Animator == null) return;
        
        Animator.SetFloat(_Speed, direction.magnitude);
    }

    public async void TakeControlToPoint(Vector3 from, Vector3 to)
    {
        takeControl = true;

        TeleportToPoint(from);
        Agent.SetDestination(to);
        UpdateAnimator();

        await UniTask.WaitUntil(() => Agent.velocity.magnitude < 0.05f);

        takeControl = false;
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
