using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorDetector : Detector
{
    [Space]
    public Doctor controller;
    [SerializeField] private float HealRange = 2f, HealDelay = 2f;

    public override HumanoidController humanController => controller;
    private Transform target => controller.target;
    private Coroutine coroutine;

    public bool Healing => coroutine != null;

    protected override void Reset()
    {
        data = null;
        controller.ResetTarget();

        StopHeal();
    }

    protected override void Change()
    {
        if(data.MaxHealth > data.Health)
        {
            controller.SetTarget(data.transform);
        }
        
        StopHeal();
    }

    protected override void Set()
    {
        if(data.MaxHealth > data.Health)
        {
            controller.SetTarget(data.transform);
        }
    }

    void Update()
    {
        if(data == null) return;
        else
        {
            if(data.MaxHealth == data.Health)
            {
                Reset();
                return;
            }
        }

        if(InColWithTargetMask || Vector3.Distance(transform.position, target.position) <= HealRange)
        {
            StartHeal();
        }
        else
        {
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

        controller.takeControl = false;
        InColWithTargetMask = false;
    }

    IEnumerator Heal()
    {
        controller.takeControl = true;

        while(true)
        {
            if(data == null || data.Died || !data.Active || data.Health == data.MaxHealth)
            {
                StopHeal();
                break;
            }

            controller.Heal(data);
            yield return new WaitForSeconds(HealDelay);
        }
    }
}
