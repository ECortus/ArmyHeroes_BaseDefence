using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorDetector : AllDetector
{
    [Space]
    [SerializeField] private Doctor controller;
    [SerializeField] private float HealTime = 5f;
    [SerializeField] private float HealRange = 2f;
    [SerializeField] private Transform HealPoint;

    public override bool AdditionalCondition(Detection dt)
    {
        return !dt.Marked && dt.Died && dt.Active;
    }

    protected override void Reset()
    {
        StopHeal();

        data = null;
        controller.ResetTarget();
        controller.ResetDestination();
    }

    protected override void Change()
    {

    }

    protected override void Set()
    {
        data.SetMarkedBy(detection);
    }

    private Coroutine coroutine;
    public bool Carring { get; set; }

    void Update()
    {
        if(controller.Died)
        {
            Stop();
            StopHeal();
            return;
        }

        if(data != null)
        {
            controller.takeControl = false;
            StartHeal();
        }
        else
        {
            controller.takeControl = true;
            StopHeal();
        }
    }

    void StartHeal()
    {
        coroutine ??= StartCoroutine(Heal());
    }

    void StopHeal()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        data?.ResetMarkedBy();
        data = null;
    }

    IEnumerator Heal()
    {
        Stop();
        HumanoidController patient = data.GetComponentInParent<HumanoidController>();

        controller.SetTarget(data.transform);
        yield return new WaitUntil(() => controller.NearPoint(data.transform.position, HealRange));
        patient.Off();
        Carring = true;

        controller.SetTarget(HealPoint);
        yield return new WaitUntil(() => controller.NearPoint(HealPoint.position, HealRange));

        Carring = false;
        controller.Off();
        yield return new WaitForSeconds(HealTime * WorkersUpgradesLVLs.DoctorHealTimeMod);

        controller.On(HealPoint.position);

        patient.On(HealPoint.position);

        Reset();
        On();

        yield return null;
    }
}
