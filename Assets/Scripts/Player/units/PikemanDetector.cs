using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikemanDetector : Detector
{
    [Space]
    public Pikeman controller;
    [SerializeField] private float MineRange = 2f, MineDelay = 2f;

    public override HumanoidController humanController => controller;
    private Transform target => controller.target;
    private Coroutine coroutine;

    public bool Mineing => coroutine != null;

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
        if(InColWithTargetMask || Vector3.Distance(transform.position, target.position) <= MineRange)
        {
            StartMine();
        }
        else
        {
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

        controller.takeControl = false;
        InColWithTargetMask = false;
    }

    IEnumerator Mine()
    {
        controller.takeControl = true;

        while(true)
        {
            if(data == null || data.Died || !data.Active)
            {
                StopMine();
                break;
            }

            controller.Mine();
            yield return new WaitForSeconds(MineDelay);
        }
    }
}
