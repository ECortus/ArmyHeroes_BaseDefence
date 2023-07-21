using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerDetector : NearestDetector
{
    [Space]
    [SerializeField] private Engineer controller;
    [SerializeField] private EngineerInfo info;
    [SerializeField] private float RepairRange = 2f, RepairDelay = 2f;

    private Transform target => controller.target;
    private Coroutine coroutine;

    public bool Repairing => coroutine != null;

    public override bool AdditionalCondition(Detection dt)
    {
        return dt != null && dt.HP < dt.MaxHP && dt.Active && !dt.Marked;
    }

    protected override void Reset()
    {
        StopRepair();

        data = null;
        controller.ResetTarget();
        controller.ResetDestination();
    }

    protected override void Change()
    {
        controller.SetTarget(data.transform);
        StopRepair();
    }

    protected override void Set()
    {
        controller.SetTarget(data.transform);
    }

    void Update()
    {
        if(controller.Died)
        {
            Reset();
            return;
        }

        if(data == null) return;
        else
        {
            Stop();
            data.Marked = true;

            if(InColWithTargetMask || controller.NearPoint(data.transform.position, RepairRange))
            {
                StartRepair();
            }
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

        if(data != null) data.Marked = false;
        controller.takeControl = false;
        InColWithTargetMask = false;
    }

    IEnumerator Repair()
    {
        controller.takeControl = true;

        while(true)
        {
            if(!(data != null && data.HP < data.MaxHP && data.Active))
            {
                break;
            }

            yield return new WaitForSeconds(RepairDelay);
            info.Repair(data);
        }

        On();
        Reset();
    }
}
