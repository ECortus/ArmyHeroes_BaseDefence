using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FirepositionUser : MonoBehaviour
{
    public bool Called = false;

    [Space]
    [SerializeField] private Detection detection;
    [SerializeField] private HumanoidController controller;

    public void On(Vector3 pos)
    {
        Called = false;
        controller.On(pos);
    }

    public void GetHit(float dmg)
    {
        detection.GetHit(dmg);
    }

    public void Off()
    {
        controller.Off();
        Depool();
    }

    public bool Pool()
    {
        return FirepositionsOperator.Instance.PoolUser(this);
    }

    public void Depool()
    {
        FirepositionsOperator.Instance.DepoolUser(this);
    }

    [SerializeField] private UnityEvent cancelOnCall;

    void CancelOnCall()
    {
        cancelOnCall?.Invoke();
    }

    public void Call(Transform position)
    {
        Debug.Log(gameObject.name + " been called");
        Called = true;

        detection.Depool();
        CancelOnCall();

        controller.SetTarget(position);
        controller.SetDestination(position.position);
    }
}
