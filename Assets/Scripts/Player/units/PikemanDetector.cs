using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PikemanDetector : AllDetector
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
        
        crystal.SetActive(false);
    }

    protected override void Change()
    {
        controller.SetTarget(data.transform);
    }

    protected override void Set()
    {
        data.SetMarkedBy(detection);

        controller.SetTarget(data.transform);
    }

    void Update()
    {
        if(detection.Died)
        {
            Reset();
            return;
        }

        if(data != null)
        {
            StartMine();
        }
    }

    void StartMine()
    {
        coroutine ??= StartCoroutine(Mine());
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
        
        data?.ResetMarkedBy();

        controller.takeControl = false;
        InColWithTargetMask = false;
        
        crystal.SetActive(false);
    }

    Transform point;

    IEnumerator Mine()
    {
        /* if(data == null || !data.Died || !data.Active)
        {
            StopMine();
        } */

        Stop();
        Detection crstl = data;

        if(crstl.GetMarkedBy() == detection)
        {
            point = crstl.transform;

            controller.SetTarget(point);
            controller.takeControl = false;

            yield return new WaitUntil(() => controller.NearPoint(point.position, mineRange));

            controller.takeControl = true;

            Mining = true;
            pickaxe.SetActive(true);

            controller.ResetTarget();
            yield return new WaitForSeconds(2f);

            pickaxe.SetActive(false);
            Mining = false;

            crstl.ResetMarkedBy();

            crystal.SetActive(true);
            Carring = true;

            controller.takeControl = false;

            point = recycle;

            controller.SetTarget(point);
            yield return new WaitUntil(() => controller.NearPoint(point.position, 1f));

            controller.ResetTarget();

            crystal.SetActive(false);
            Carring = false;

            Crystal.Plus(1 + WorkersUpgradesLVLs.PikemanResourceMod);
        }

        Reset();
        On();

        yield return null;
    }
}
