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

    [Space] [SerializeField] private bool EnableGo = true;
    [SerializeField] private float GoDelay = 10f;

    private Transform[] GoDots => LevelManager.Instance.ActualLevel.SoldiersGoDots;
    float time = 0f;

    void Update()
    {
        if (data != null || !EnableGo) return;
        
        time -= Time.deltaTime;

        if(time <= 0f)
        {
            Go();
            time = 9999f;
        }
    }

    public void Go()
    {
        GoToRandomPoint();
    }

    public void GoToRandomPoint()
    {
        /*Vector3 point = transform.position;
        point += Random.insideUnitSphere.normalized * Random.Range(1f, 5f);
        point.y = transform.position.y;*/

        Vector3 point = GoDots[Random.Range(0, GoDots.Length)].position;
        
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

        time = GoDelay;
    } 
}
