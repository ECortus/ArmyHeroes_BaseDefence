using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class SoldierDetector : NearestDetector
{
    [Space]
    [SerializeField] private Soldier controller;
    [SerializeField] private Shooting shooting;
    [SerializeField] private bool TakeControl = false;

    [Range(0, 100)]
    [SerializeField] private int ChanceToChangePosition = 0;

    protected override bool AdditionalConditionToData(Detection dt)
    {
        return !dt.Died && dt.Active;
    }

    protected override void Reset()
    {
        data = null;
        controller.ResetTarget();
        controller.takeControl = false;

        shooting.Disable();
    }

    protected override void Change()
    {
        if(data != null)
        {
            Set();
        }
        else
        {
            Reset();
        }

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

    float time = 0f;

    void Update()
    {
        time -= Time.deltaTime;

        if(data == null && time <= 0f)
        {
            TryGo();
            time = 1f;
        }
    }

    public void TryGo()
    {
        int i = Random.Range(0, 100);
        if(ChanceToChangePosition >= i)
        {
            GoToRandomPoint();
        }
    }

    public void GoToRandomPoint()
    {
        Vector3 point = transform.position;
        point += Random.insideUnitSphere.normalized * Random.Range(1f, 5f);
        point.y = transform.position.y;

        GoToPoint(point);
    }

    public async void GoToPoint(Vector3 point)
    {
        Stop();

        data = null;
        controller.ResetTarget();
        controller.takeControl = false;

        shooting.Disable();

        controller.SetDestination(point);

        await UniTask.WaitUntil(() => controller.NearPoint(point, 0.5f));
        On();
    }
}
