using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Gate : MonoBehaviour
{
    public bool Opened { get; set; }
    public bool SomeoneOnDoor { get; set; }

    [Space]
    [SerializeField] private Transform gate;
    private float closedGateRotate;
    private float openedGateRotate;
    private float gateRotateAngle;

    [SerializeField] private float timeToClose, rotateSpeed;
    Quaternion rot;

    void OnEnable()
    {
        WriteCloseAngle();
        SetRotate(closedGateRotate, out gateRotateAngle);

        Reset();
    }

    void OnDisable()
    {
        Reset();
    }

    void Update()
    {
        RotateGates();
    }

    void RotateGates()
    {
        rot = Quaternion.Euler(
            gate.transform.localEulerAngles.x, 
            gateRotateAngle, 
            gate.transform.localEulerAngles.z
        );

        gate.localRotation = Quaternion.Slerp(gate.localRotation, rot, rotateSpeed * Time.deltaTime);
    }

    void Reset()
    {
        rot = Quaternion.Euler(
            gate.transform.localEulerAngles.x, 
            closedGateRotate, 
            gate.transform.localEulerAngles.z
        );

        gate.localRotation = rot;
}

    async void Open(Transform opener)
    {
        SetRotate(closedGateRotate, out openedGateRotate);

        Vector3 dir = (transform.position - opener.position).normalized;
        float angle = Vector3.Angle(transform.forward, dir);

        if(angle < 90f)
        {
            openedGateRotate += 90f;
        }
        else
        {
            openedGateRotate -= 90f;
        }

        SetRotate(openedGateRotate, out gateRotateAngle);
        Opened = true;

        await UniTask.Delay((int)(timeToClose * 1000));

        if(SomeoneOnDoor)
        {
            await UniTask.WaitUntil(() => !SomeoneOnDoor);
        }
        Close();
    }

    public async void Close()
    {
        SetRotate(closedGateRotate, out gateRotateAngle);

        await UniTask.Delay(1000);

        Opened = false;
    }

    void WriteCloseAngle()
    {
        closedGateRotate = gate.localEulerAngles.y;
    }

    void SetRotate(float angle, out float outAngles)
    {
        outAngles = angle;;
    }

    void OnTriggerStay(Collider col)
    {
        GameObject go = col.gameObject;

        switch(go.tag)
        {
            case "Player":
                SomeoneOnDoor = true;
                if(Opened) return;
                else
                {
                    Open(Player.Instance.Transform);
                }
                break;
            case "Ally":
                SomeoneOnDoor = true;
                if(Opened) return;
                else
                {
                    Open(go.transform);
                }
                break;
            default:
                break;
        }
    }

    void OnTriggerExit(Collider col)
    {
        GameObject go = col.gameObject;

        switch(go.tag)
        {
            case "Player":
                SomeoneOnDoor = false;
                break;
            case "Ally":
                SomeoneOnDoor = false;
                break;
        }
    }
}
