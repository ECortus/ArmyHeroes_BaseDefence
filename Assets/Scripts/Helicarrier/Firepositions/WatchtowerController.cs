using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchtowerController : Target
{
    [SerializeField] private Transform[] toRotatesTransform;
    [SerializeField] private Transform[] toRotatesMuzzles;

    private Transform toRotateTransform => toRotatesTransform[upgrader.Progress];
    private Transform toRotateMuzzle => toRotatesTransform[upgrader.Progress];

    [SerializeField] private float rotateSpeed;

    [Space]
    [SerializeField] private Fireposition fireposition;
    [SerializeField] private FirepositionUpgrader upgrader;

    Vector3 dir;
    Quaternion targetRotation;

    void Update()
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

                toRotateMuzzle.rotation = Quaternion.LookRotation(dir);
                
                dir.y = 0f;
            }

            targetRotation = Quaternion.LookRotation(dir);

            toRotateTransform.rotation = Quaternion.RotateTowards(
                toRotateTransform.rotation, targetRotation, Time.deltaTime * rotateSpeed
            );
        }
    }
}
