using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorDetector : AllDetector
{
    [Space]
    [SerializeField] private Doctor controller;
    [SerializeField] private float HealRange = 2f;
    [SerializeField] private Transform HealPoint;

    public override bool AdditionalCondition(Detection dt)
    {
        return !dt.Marked && dt.Died && dt.Active;
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
        if(coroutine == null) coroutine = StartCoroutine(Heal());
    }

    void StopHeal()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        if(data != null) data.Marked = false;
        data = null;
    }

    IEnumerator Heal()
    {
        Stop();
        HumanoidController patient = data.GetComponentInParent<HumanoidController>();
        data.Marked = true;

        controller.SetTarget(data.transform);
        yield return new WaitUntil(() => controller.NearPoint(data.transform.position, HealRange));
        patient.Off();
        Carring = true;

        controller.SetTarget(HealPoint);
        yield return new WaitUntil(() => controller.NearPoint(HealPoint.position, HealRange));

        /* controller.Off(); */
        yield return new WaitForSeconds(2f);

        /* controller.On(HealPoint.position); */

        patient.On(HealPoint.position);
        data.Marked = false;

        controller.ResetTarget();
        controller.ResetDestination();
        Carring = false;

        yield return new WaitForSeconds(1f);

        On();

        StopHeal();

        yield return null;
    }
}
