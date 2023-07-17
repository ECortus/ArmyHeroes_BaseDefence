using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikemanDetector : NearestDetector
{
    [Space]
    [SerializeField] private Pikeman controller;
    [SerializeField] private float mineRange = 2f;

    protected override bool AdditionalConditionToData(Detection dt)
    {
        return !dt.Died && dt.Active && !dt.Marked;
    }

    private Coroutine coroutine;

    public bool Mining { get; set; }
    public bool Carring { get; set; }

    [SerializeField] private Transform recyclePoint;
    [SerializeField] private GameObject crystal;

    protected override void Reset()
    {
        data = null;
        controller.ResetTarget();

        StopMine();
    }

    protected override void Change()
    {
        controller.SetTarget(data.transform);
        StopMine();
    }

    protected override void Set()
    {
        controller.SetTarget(data.transform);
    }

    void Update()
    {
        if(detection.Died)
        {
            StopMine();
            return;
        }

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
        }

        Mining = false;
        Carring = false;

        data = null;
        controller.takeControl = false;
        InColWithTargetMask = false;
    }

    IEnumerator Mine()
    {
        /* if(data == null || !data.Died || !data.Active)
        {
            StopMine();
        } */

        Stop();
        Detection crstl = data;

        if(!crstl.Marked)
        {
            controller.SetTarget(crstl.transform);
            controller.takeControl = false;

            crstl.Marked = true;

            yield return new WaitUntil(() => controller.NearPoint(crstl.transform.position, mineRange) || InColWithTargetMask);

            Mining = true;
            controller.ResetTarget();
            yield return new WaitForSeconds(2f);
            Mining = false;

            crystal.SetActive(true);
            Carring = true;

            controller.SetTarget(recyclePoint);
            yield return new WaitUntil(() => controller.NearPoint(recyclePoint.position,mineRange));

            controller.ResetTarget();
            yield return new WaitForSeconds(1f);

            crystal.SetActive(false);
            Carring = false;

            Crystal.Plus(1);
            crstl.GetHit(1f);

            crstl.Marked = false;
        }

        Reset();
        StopMine();
        On();

        yield return null;
    }
}
