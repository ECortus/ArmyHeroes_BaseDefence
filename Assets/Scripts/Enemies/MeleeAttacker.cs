using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttacker : Detector
{
    [Space]
    public Enemy controller;
    [SerializeField] private float attackRange = 2f, attackDelay = 2f;

    public override HumanoidController humanController => controller;
    private Transform target => controller.target;
    private Coroutine coroutine;

    public bool Fighting => coroutine != null;

    protected override void Reset()
    {
        data = null;
        controller.SetMainTarget(controller.MainTarget);

        StopAttack();
    }

    protected override void Change()
    {
        controller.SetAdditionalTarget(data.transform);
    }

    protected override void Set()
    {
        controller.SetAdditionalTarget(data.transform);
    }

    void Update()
    {
        if(InColWithTargetMask || Vector3.Distance(transform.position, target.position) <= attackRange)
        {
            controller.takeControl = true;
            StartAttack();
        }
        else
        {
            controller.takeControl = false;
            StopAttack();
        }
    }

    void StartAttack()
    {
        if(coroutine == null) coroutine = StartCoroutine(Attack());
    }

    void StopAttack()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        InColWithTargetMask = false;
    }

    IEnumerator Attack()
    {
        while(true)
        {
            if(data == null || data.Died || !data.Active)
            {
                StopAttack();
                break;
            }

            controller.Attack(data);

            yield return new WaitForSeconds(attackDelay);
        }
    }
}
