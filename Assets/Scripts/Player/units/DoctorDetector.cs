using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DoctorDetector : AllDetector
{
    [Space]
    [SerializeField] private Doctor controller;

    [SerializeField] private GameObject toOnOff;
    [SerializeField] private float HealTime = 5f;
    [SerializeField] private float HealRange = 2f;
    
    private DoctorPatientTimers Timers => LevelManager.Instance.ActualLevel.PatientTimers;
    private Transform HealPoint => LevelManager.Instance.ActualLevel.HealPoint;

    [Space] [SerializeField] private DoctorHealSlider healSlider;

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
        if (!controller.Active || controller.Died)
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

        CurrentHealTime = 0;
        
        data?.ResetMarkedBy();
        data = null;
    }

    [HideInInspector] public float CurrentHealTime = 0;
    public float MaxHealTime => HealTime * WorkersUpgradesLVLs.DoctorHealTimeMod;

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
        /*controller.Off();*/
        
        toOnOff.SetActive(false);
        CurrentHealTime = MaxHealTime;
        healSlider.On();
        
        Timers.RefreshHealAllZone();

        controller.takeControl = true;

        while (CurrentHealTime > 0)
        {
            CurrentHealTime -= Time.deltaTime;
            
            if (Timers.AllHealed) CurrentHealTime = 0;
            
            healSlider.Refresh();
            yield return null;
        }

        healSlider.Off();
        toOnOff.SetActive(true);
        controller.On(HealPoint.position);
        controller.takeControl = false;
        
        Timers.RefreshHealAllZone();

        patient.On(HealPoint.position);

        Reset();
        On();

        yield return null;
    }
}
