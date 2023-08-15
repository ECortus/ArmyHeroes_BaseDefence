using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttacker : NearestDetector
{
    [Space]
    [SerializeField] private Enemy controller;
    [SerializeField] private EnemyInfo info;
    [SerializeField] private float attackRange = 2f, attackDelay = 1.75f;

    private Coroutine coroutine;

    public bool Fighting = false;

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
        if(controller.Died)
        {
            data = null;
            StopAttack();
            return;
        }

        if(data != null)
        {
            StartAttack();
        }
    }

    void StartAttack()
    {
        coroutine ??= StartCoroutine(Attack());
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
        while(true)
        {
            if(!AdditionalCondition(data))
            {
                break;
            }

            if(controller.NearPoint(data.transform.position, attackRange) || InColWithTargetMask)
            {
                controller.takeControl = true;
                Fighting = true;

                yield return new WaitForSeconds(attackDelay / 2f);
                info.Attack(data);
                yield return new WaitForSeconds(attackDelay / 2f);
            }
            else
            {
                Fighting = false;
                controller.takeControl = false;
            }

            yield return null;
        }

        StopAttack();
    }
}
