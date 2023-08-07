using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarController : Target
{
    /* [SerializeField] private Transform[] toRotates;
    private Transform toRotate => toRotates[upgrader.Progress];

    [SerializeField] private float rotateSpeed; */

    [Space]
    [SerializeField] private Fireposition fireposition;
    [SerializeField] private FirepositionUpgrader upgrader;

    Vector3 dir;
    Vector3 angles;
    Quaternion targetRotation;

    /* void Update()
    {
        if(fireposition.Busy)
        {
            if(target == transform)
            {
                dir = -transform.forward;
            }
            else
            {
                dir = -(target.position - transform.position).normalized;
                dir.y = 0f;
            }

            targetRotation = Quaternion.LookRotation(dir);
            angles = targetRotation.eulerAngles;
            angles.x += -90f;

            toRotate.rotation = Quaternion.RotateTowards(
                toRotate.rotation, Quaternion.Euler(angles), Time.deltaTime * rotateSpeed
            );
        }
    } */
}
