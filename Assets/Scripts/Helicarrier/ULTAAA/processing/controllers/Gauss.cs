using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gauss : Target
{
    [SerializeField] private Transform torotate;
    [SerializeField] private float rotateSpeed;

    Vector3 dir;
    Quaternion targetRotation;

    void Update()
    {
        dir = (target.position + new Vector3(0f, 1f, 0f) - torotate.position).normalized;

        targetRotation = Quaternion.LookRotation(dir);

        torotate.rotation = Quaternion.RotateTowards(
            torotate.rotation, targetRotation, Time.deltaTime * rotateSpeed
        );
    }
}
