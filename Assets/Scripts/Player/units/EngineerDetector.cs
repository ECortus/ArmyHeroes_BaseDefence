using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerDetector : Detector
{
    [Space]
    public Engineer controller;
    [SerializeField] private float RepairRange = 2f, RepairDelay = 2f;

    public override HumanoidController humanController => controller;
    private Transform target => controller.target;
    private Coroutine coroutine;

    public bool Repairing => coroutine != null;

    protected override void Reset()
    {
        data = null;
        controller.ResetTarget();

        StopRepair();
    }

    protected override void Change()
    {
        controller.SetTarget(data.transform);
    }

    protected override void Set()
    {
        controller.SetTarget(data.transform);
    }

    void Update()
    {
        if(data == null) return;
        else
        {
            if(data.MaxHealth == data.Health)
            {
                controller.takeControl = true;
                Reset();
                return;
            }
            else
            {
                controller.takeControl = false;
            }
        }

        if(InColWithTargetMask || Vector3.Distance(transform.position, target.position) <= RepairRange)
        {
            controller.takeControl = true;
            StartRepair();
        }
        else
        {
            controller.takeControl = false;
            StopRepair();
        }
    }

    void StartRepair()
    {
        if(coroutine == null) coroutine = StartCoroutine(Repair());
    }

    void StopRepair()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        InColWithTargetMask = false;
    }

    IEnumerator Repair()
    {
        while(true)
        {
            if(data == null || data.Died || !data.Active)
            {
                StopRepair();
                break;
            }

            controller.Repair(data);
            yield return new WaitForSeconds(RepairDelay);
        }
    }
}
