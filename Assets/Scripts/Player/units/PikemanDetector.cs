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

        if(data == null) return;
        else
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
            SendToCrystal(crstl);

            yield return new WaitUntil(() => controller.NearPoint(point.position, mineRange));

            StartMining();
            yield return new WaitForSeconds(2f);

            StopMining(crstl);

            SendToRecycle();
            yield return new WaitUntil(() => controller.NearPoint(point.position, 1f));

            Recycle();
        }

        Reset();
        On();

        yield return null;
    }

    void SendToCrystal(Detection crstl)
    {
        point = crstl.transform;

        controller.SetTarget(point);
        controller.takeControl = false;
    }

    void StartMining()
    {
        controller.takeControl = true;

        Mining = true;
        pickaxe.SetActive(true);

        controller.ResetTarget();
    }

    void StopMining(Detection crstl)
    {
        pickaxe.SetActive(false);
        Mining = false;

        crstl.ResetMarkedBy();
    }

    void SendToRecycle()
    {
        crystal.SetActive(true);
        Carring = true;

        controller.takeControl = false;

        point = recycle;

        controller.SetTarget(point);
    }

    void Recycle()
    {
        controller.ResetTarget();

        crystal.SetActive(false);
        Carring = false;

        Crystal.Plus(1 + WorkersUpgradesLVLs.PikemanResourceMod);
    }
}
