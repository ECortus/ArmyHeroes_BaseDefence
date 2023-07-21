using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttacker : NearestDetector
{
    [Space]
    [SerializeField] private Enemy controller;
    [SerializeField] private EnemyInfo info;
    [SerializeField] private float attackRange = 2f, preAttackDelay = 1f, attackDelay = 1.5f;

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

    public override bool AdditionalCondition(Detection dt)
    {
        return dt != null && !dt.Died && dt.Active;
    }

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
            StartAttack();
        }
        else
        {
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
            if(!AdditionalCondition(data))
            {
                break;
            }

            yield return new WaitForSeconds(preAttackDelay);
            info.Attack(data);
            yield return new WaitForSeconds(attackDelay);
        }

        StopAttack();
    }
}
