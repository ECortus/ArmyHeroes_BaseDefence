using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StutteterDetector : NearestDetector
{
    [Space]
    [SerializeField] private float angle;
    [SerializeField] private Target trg;
    [SerializeField] private GunHandler shooting;

    public override bool AdditionalCondition(Detection dt)
    {
        return !dt.Died && dt.Active 
            && Vector3.Angle(transform.forward, (dt.transform.position - transform.position).normalized) < angle / 2f;
    }

    protected override void Reset()
    {
        data = null;
        trg.ResetTarget();

        shooting.Disable();
    }

    protected override void Change()
    {
        trg.SetTarget(data.transform);
        /* shooting.Disable(); */
    }

    protected override void Set()
    {
        trg.SetTarget(data.transform);
        if(!shooting.isEnable)
        {
            shooting.Enable();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(transform.position, range);

        Vector3 viewAngle01 = DirectionFromAngle(transform.eulerAngles.y, -angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(transform.eulerAngles.y, angle / 2);

        Gizmos.DrawLine(transform.position, transform.position + viewAngle01 * range);
        Gizmos.DrawLine(transform.position, transform.position + viewAngle02 * range);
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
