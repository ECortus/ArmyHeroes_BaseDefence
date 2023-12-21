using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class SoldierDetector : NearestDetector
{
    [Space]
    [SerializeField] private Soldier controller;
    [SerializeField] private GunHandler shooting;
    [SerializeField] private bool TakeControl = false;

    public override bool AdditionalCondition(Detection dt)
    {
        return !dt.Died && dt.Active;
    }

    protected override void Reset()
    {
        data = null;
        controller.ResetTarget();
        controller.ResetDestination();

        controller.takeControl = false;
        
        shooting.Disable();
        
        CancelGo(false);
    }

    protected override void Change()
    {
        controller.SetTarget(data.transform);
        controller.takeControl = TakeControl;

        /* shooting.Disable(); */
    }

    protected override void Set()
    {
        controller.SetTarget(data.transform);
        controller.takeControl = TakeControl;

        if(!shooting.isEnable)
        {
            shooting.Enable();
        }
    }

    [Space]
    [SerializeField] private float GoDelay = 10f;

    private Transform[] GoDots => LevelManager.Instance.ActualLevel.SoldiersGoDots;
    float time = 2f;

    void Update()
    {
        if (!controller.Active || controller.Died)
        {
            Reset();
            return;
        }
        
        time -= Time.deltaTime;

        if(time <= 0f)
        {
            Go();
            time = 99999f;
        }
    }

    private Coroutine goCoroutine;

    public void Go()
    {
        GoToRandomPoint();
    }

    public void GoToRandomPoint()
    {
        Vector3 point = GoDots[Random.Range(0, GoDots.Length)].position;
        goCoroutine ??= StartCoroutine(GoToPoint(GoDots[Random.Range(0, GoDots.Length)]));
    }

    public void CancelGo(bool on = true)
    {
        if(on) On();

        if (goCoroutine != null)
        {
            StopCoroutine(goCoroutine);
            goCoroutine = null;
        }
        
        time = GoDelay;
        if(data) Set();
    }

    IEnumerator GoToPoint(Transform dot)
    {
        Stop();

        data = null;
        controller.ResetTarget();
        controller.takeControl = false;

        shooting.Disable();

        controller.SetTarget(dot);
        controller.takeControl = false;

        yield return new WaitUntil(() =>
            controller.NearPoint(controller.target.position, 2f) || !DetectorOn);
        
        CancelGo();
    }
}
