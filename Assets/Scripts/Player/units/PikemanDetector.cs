using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PikemanDetector : NearestDetector
{
    [Space]
    [SerializeField] private Pikeman controller;
    [SerializeField] private float mineRange = 2f;

    public override bool AdditionalCondition(Detection dt)
    {
        return !dt.Died && dt.Active && !dt.Marked;
    }

    private Coroutine coroutine;

    public bool Mining { get; set; }
    public bool Carring { get; set; }

    [Space]
    [SerializeField] private Transform[] entersToRecycle;

    private Transform GetClosest(Transform[] array)
    {
        List<Transform> list = array.ToList();
        list = list.OrderBy(x => (x.transform.position - transform.position).magnitude).ToList();

        return list[0];
    }

    [SerializeField] private Transform recycle;

    [Space]
    [SerializeField] private GameObject pickaxe;
    [SerializeField] private GameObject crystal;

    protected override void Reset()
    {
        StopMine();

        data = null;
        controller.ResetTarget();
        controller.ResetDestination();
    }

    protected override void Change()
    {
        controller.SetTarget(data.transform);
    }

    protected override void Set()
    {
        controller.SetTarget(data.transform);
    }

    void Update()
    {
        if(detection.Died)
        {
            Reset();
            return;
        }

        if(data == null) return;
        else
        {
            StartMine();
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
        if(data != null) data.Marked = false;

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
        Transform point = null;

        if(!crstl.Marked)
        {
            point = crstl.transform;

            controller.SetTarget(point);
            controller.takeControl = false;

            crstl.Marked = true;

            yield return new WaitUntil(() => controller.NearPoint(point.position, mineRange) || InColWithTargetMask);

            controller.takeControl = true;

            Mining = true;
            pickaxe.SetActive(true);

            controller.ResetTarget();
            yield return new WaitForSeconds(2f);

            pickaxe.SetActive(false);
            Mining = false;

            crystal.SetActive(true);
            Carring = true;

            controller.takeControl = false;

            point = GetClosest(entersToRecycle);

            controller.SetTarget(point);
            yield return new WaitUntil(() => controller.NearPoint(point.position, 1f));

            point = recycle;

            controller.SetTarget(point);
            yield return new WaitUntil(() => controller.NearPoint(point.position, 1f));

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
