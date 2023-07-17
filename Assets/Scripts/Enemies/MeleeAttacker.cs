using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttacker : NearestDetector
{
    [Space]
    [SerializeField] private Enemy controller;
    [SerializeField] private EnemyInfo info;
    [SerializeField] private float attackRange = 2f, preAttackDelay = 1f, attackDelay = 1.5f;

    private Transform target => controller.target;
    private bool TargetInAttackRange
    {
        get
        {
            if(data == null) return false;
            else return Vector3.Distance(transform.position, data.transform.position) <= attackRange;
        }
    }
    private Coroutine coroutine;

    public bool Fighting => coroutine != null;

    protected override bool AdditionalConditionToData(Detection dt) => !dt.Died;

    protected override void Reset()
    {
        data = null;
        controller.SetMainTarget(controller.MainTarget);

        StopAttack();
    }

    protected override void Change()
    {
        controller.SetAdditionalTarget(data.transform);
        StopAttack();
    }

    protected override void Set()
    {
        controller.SetAdditionalTarget(data.transform);
    }

    void Update()
    {
        if(InColWithTargetMask || TargetInAttackRange)
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

        controller.takeControl = false;
        InColWithTargetMask = false;
    }

    IEnumerator Attack()
    {
        controller.takeControl = true;
        
        while(true)
        {
            if(data == null || data.Died || !data.Active)
            {
                StopAttack();
                break;
            }
            yield return new WaitForSeconds(preAttackDelay);
            info.Attack(data);
            yield return new WaitForSeconds(attackDelay);
        }
    }
}
