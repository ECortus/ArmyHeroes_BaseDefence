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
    float time = 2f;

    void Update()
    {
        if (!controller.Active || controller.Died)
        {
            Reset();
            return;
        }
        
        if (data != null || !EnableGo || !isOn)
        {
            CancelGo();
        }
        
        time -= Time.deltaTime;

        if(time <= 0f)
        {
            Go();
            time = 99999f;
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
        
        GoToPoint(GoDots[Random.Range(0, GoDots.Length)]);
    }

    void CancelGo()
    {
        if(gameObject.activeSelf && controller.Active && !controller.Died) On();
        time = GoDelay;
    }

    public async void GoToPoint(Transform dot)
    {
        Stop();

        data = null;
        controller.ResetTarget();
        controller.takeControl = false;

        shooting.Disable();

        controller.SetTarget(dot);
        controller.takeControl = false;

        await UniTask.WaitUntil(() => controller.NearPoint(controller.target.position, 2f) /*|| isOn || !gameObject.activeSelf*/);
        
        CancelGo();
    } 
}
