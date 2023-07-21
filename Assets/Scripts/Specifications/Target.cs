using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour
{
    public Transform _target { get; set; }
    public Transform target
    {
        get
        {
            if(_target == null) return transform;
            else return _target;
        }
        set
        {
            _target = value;
        }
    }

    public Vector3 DirectionToTarget
    {
        get
        {
            if(_target == null) return Vector3.zero;

            Vector3 dir = (_target.position - transform.position).normalized;
            dir.y = 0f;
            return dir;
        }
    }

    public void SetTarget(Transform trg)
    {
        _target = trg;
    }

    public void ResetTarget()
    {
        _target = null;
    }

    public bool takeControl { get; set; }
}
