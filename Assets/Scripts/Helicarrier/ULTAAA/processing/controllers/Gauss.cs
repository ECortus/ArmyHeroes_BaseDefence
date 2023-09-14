using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gauss : Target
{
    [SerializeField] private Transform torotate;
    [SerializeField] private float rotateSpeed;

    Vector3 dir, angles;
    Quaternion targetRotation;

    void Update()
    {
        if (target != transform)
        {
            dir = (target.position + new Vector3(0f, 1f, 0f) - torotate.position).normalized;
        }
        else
        {
            dir = Vector3.forward;
        }
        
        targetRotation = Quaternion.LookRotation(dir);

        torotate.rotation = Quaternion.RotateTowards(
            torotate.rotation, targetRotation, Time.deltaTime * rotateSpeed
        );
    }
}
