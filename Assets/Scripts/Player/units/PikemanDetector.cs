using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikemanDetector : Detector
{
    [Space]
    public Pikeman controller;
    [SerializeField] private float mineRange = 2f;

    public override HumanoidController humanController => controller;
    private Transform target => controller.target;
    private Coroutine coroutine;

    public bool Mining => coroutine != null;

    [SerializeField] private Transform recyclePoint;
    [SerializeField] private GameObject crystal;

    protected override void Reset()
    {
        if(!Mining)
        {
            data = null;
            controller.ResetTarget();

            StopMine();
        }
    }

    protected override void Change()
    {
        if(!Mining) controller.SetTarget(data.transform);
    }

    protected override void Set()
    {
        if(!Mining) controller.SetTarget(data.transform);
    }

    void Update()
    {
        if(data != null)
        {
            controller.takeControl = false;
            StartMine();
        }
        else
        {
            controller.takeControl = true;
            StopMine();
        }
    }

    void StartMine()
    {
        if(coroutine == null) coroutine = StartCoroutine(Mine());
    }

    void StopMine()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;

            data = null;
        }

        InColWithTargetMask = false;
    }

    IEnumerator Mine()
    {
        /* if(data == null || !data.Died || !data.Active)
        {
            StopMine();
        } */
        Info crstl = data;

        controller.SetTarget(crstl.transform);
        controller.takeControl = false;

        yield return new WaitUntil(() => 
            Vector3.Distance(transform.position, crstl.transform.position) < mineRange || InColWithTargetMask);

        controller.ResetTarget();
        yield return new WaitForSeconds(1f);

        crystal.SetActive(true);

        controller.SetTarget(recyclePoint);
        yield return new WaitUntil(() => Vector3.Distance(transform.position, recyclePoint.position) < mineRange);

        controller.ResetTarget();
        yield return new WaitForSeconds(1f);

        crystal.SetActive(false);
        Crystal.Plus(1);
        crstl.GetHit(1f);

        Reset();
        StopMine();

        yield return null;
    }
}
