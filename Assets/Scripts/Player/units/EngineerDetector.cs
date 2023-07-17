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

    protected override bool AdditionalConditionToData(Detection dt)
    {
        return dt != null && dt.MaxHP > dt.HP && !dt.Died && dt.Active;
    }

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

        if(InColWithTargetMask || controller.NearPoint(data.transform.position, RepairRange))
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
            if(AdditionalConditionToData(data))
            {
                StopRepair();
                break;
            }

            info.Repair(data);
            yield return new WaitForSeconds(RepairDelay);
        }
    }
}
