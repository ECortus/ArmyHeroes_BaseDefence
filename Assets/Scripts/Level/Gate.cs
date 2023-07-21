using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Gate : MonoBehaviour
{
    public bool Opened { get; set; }
    public bool SomeoneOnDoor { get; set; }

    [Space]
    [SerializeField] private Transform[] gates;
    private List<float> closedGateRotate = new List<float>();
    private List<float> openedGateRotate = new List<float>();
    private List<float> gateRotateAngle = new List<float>();

    [SerializeField] private float timeToClose, rotateSpeed;
    Quaternion rot;

    void OnEnable()
    {
        WriteCloseAngle();
        SetRotateList(closedGateRotate, out gateRotateAngle);

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
        int i = 0;
        foreach(Transform gate in gates)
        {
            rot = Quaternion.Euler(
                gate.transform.localEulerAngles.x, 
                gateRotateAngle[i], 
                gate.transform.localEulerAngles.z
            );

            if(gate.localEulerAngles.y == gateRotateAngle[i])
            {
                i++;
                continue;
            }

            gate.localRotation = Quaternion.Slerp(gate.localRotation, rot, rotateSpeed * Time.deltaTime);
            i++;
        }
    }

    void Reset()
    {
        int i = 0;
        foreach(Transform gate in gates)
        {
            rot = Quaternion.Euler(
                gate.transform.localEulerAngles.x, 
                closedGateRotate[i], 
                gate.transform.localEulerAngles.z
            );

            gate.localRotation = rot;
            i++;
        }
    }

    async void Open(Transform opener)
    {
        SetRotateList(closedGateRotate, out openedGateRotate);

        Vector3 dir = (transform.position - opener.position).normalized;
        float angle = Vector3.Angle(transform.forward, dir);
        if(angle < 90f)
        {
            for(int i = 0; i < openedGateRotate.Count; i++)
            {
                if(closedGateRotate[i] >= 180f) openedGateRotate[i] -= 90f;
                else openedGateRotate[i] += 90f;
            }
        }
        else
        {
            for(int i = 0; i < openedGateRotate.Count; i++)
            {
                if(closedGateRotate[i] >= 180f) openedGateRotate[i] += 90f;
                else openedGateRotate[i] -= 90f;
            }
        }

        SetRotateList(openedGateRotate, out gateRotateAngle);
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
        SetRotateList(closedGateRotate, out gateRotateAngle);

        await UniTask.Delay(1000);

        Opened = false;
    }

    void WriteCloseAngle()
    {
        closedGateRotate.Clear();
        foreach(Transform gate in gates)
        {
            closedGateRotate.Add(gate.localEulerAngles.y);
        }
    }

    void SetRotateList(List<float> angles, out List<float> outAngles)
    {
        List<float> list = new List<float>();
        foreach(float angle in angles)
        {
            list.Add(angle);
        }

        outAngles = list;
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
