using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerDetector : AllDetector
{
    [Space]
    [SerializeField] private Engineer controller;
    [SerializeField] private EngineerInfo info;
    [SerializeField] private float RepairRange = 2f, RepairDelay = 2f;

    private Transform target => controller.target;
    private Coroutine coroutine;

    [HideInInspector] public bool Repairing = false;

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
        data.SetMarkedBy(detection);

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
            StartRepair();
        }

        /* Debug.Log($"{gameObject.name}: {data != null} {(data != null ? ", data: " + data.transform.parent.name + ": " + data.name : "")}"); */
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

        data?.ResetMarkedBy();
        Repairing = false;
        controller.takeControl = false;
    }

    IEnumerator Repair()
    {
        Stop();

        while(true)
        {
            if(data == null || data.HP >= data.MaxHP || !data.Active)
            {
                break;
            }

            if(/* InColWithTargetMask ||  */controller.NearPoint(data.transform.position, RepairRange))
            {
                Repairing = true;
                controller.takeControl = true;

                yield return new WaitForSeconds(RepairDelay * WorkersUpgradesLVLs.EngineerRepairSpeedMod);
                info.Repair(data);
            }
            else
            {
                Repairing = false;
                controller.takeControl = false;
            }

            yield return null;
        }

        Repairing = false;
        data.ResetMarkedBy();

        Reset();
        On();
    }
}
