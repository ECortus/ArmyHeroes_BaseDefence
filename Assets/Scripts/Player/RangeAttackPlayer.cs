using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class RangeAttackPlayer : PlayerNearestDetector
{
    [Space]
    [SerializeField] private Player controller;
    [SerializeField] private GunHandler shooting;

    [Space]
    [SerializeField] private Transform leftShoulder;
    [SerializeField] private Transform rightShoulder;

    public class TransformPair
    {
        public Transform First, Second;
    }

    private TransformPair _Weapons;
    private TransformPair Weapons
    {
        get
        {
            if (_Weapons == null)
            {
                _Weapons = new TransformPair();
            }

            _Weapons.First = shooting.CurrentComplex.Pair[0].Gun.transform;
            _Weapons.Second = shooting.CurrentComplex.Pair[1].Gun.transform;

            return _Weapons;
        }
    }

    private TransformPair _Shoulders;
    private TransformPair Shoulders
    {
        get
        {
            if (_Shoulders == null)
            {
                _Shoulders = new TransformPair();
            }

            _Shoulders.First = rightShoulder;
            _Shoulders.Second = leftShoulder;

            return _Shoulders;
        }
    }

    public override bool AdditionalCondition(Detection dt)
    {
        return !dt.Died && dt.Active 
            /*&& Vector3.Angle(transform.forward, (dt.transform.position - transform.position).normalized) < angle / 2f*/;
    }

    protected override void Reset()
    {
        data = null;
        addit_data = null;
        
        controller.ResetTarget();
        controller.takeControl = false;

        shooting.Disable();
        controller.ResetRotateDir();
    }

    protected override void Change()
    {
        controller.SetTarget(data.transform);
        controller.takeControl = false;

        /* shooting.Disable(); */
    }

    protected override void Set()
    {
        controller.SetTarget(data.transform);
        if(!shooting.isEnable)
        {
            shooting.Enable();
        }
    }

    void Update()
    {
        RotateWeapons();
    }

    void RotateWeapons()
    {
        Vector3 direction;
        Vector3 shoulderDirection;
        Transform weapon;
        
        if (data != null)
        {
            weapon = Weapons.First;
            
            direction = (data.transform.position - weapon.position).normalized;
            shoulderDirection = (data.transform.position - Shoulders.First.position).normalized;
            
            RotateTransformTowards(Shoulders.First, shoulderDirection);
            RotateTransformTowards(weapon, direction);

            weapon = Weapons.Second;

            if (addit_data != null)
            {
                direction = (addit_data.transform.position - weapon.position).normalized;
                shoulderDirection = (addit_data.transform.position - Shoulders.Second.position).normalized;
                controller.SetRotateDir(MiddleRotateDir());
            }
            else
            {
                direction = (data.transform.position - weapon.position).normalized;
                shoulderDirection = (data.transform.position - Shoulders.Second.position).normalized;
                controller.ResetRotateDir();
            }
            
            RotateTransformTowards(Shoulders.Second, shoulderDirection);
            RotateTransformTowards(weapon, direction);
        }
        else
        {
            direction = transform.forward;
            
            RotateTransformTowards(Shoulders.First, direction);
            RotateTransformTowards(Shoulders.Second, direction);

            RotateTransformTowards(Weapons.First, direction);
            RotateTransformTowards(Weapons.Second, direction);
        }
    }

    Vector3 MiddleRotateDir()
    {
        Vector3 first = data.transform.position;
        Vector3 second = addit_data.transform.position;

        Vector3 middle = first + (second - first).normalized * Vector3.Distance(first, second) / 2f;

        return (middle - transform.position).normalized;
    }
    
    private void RotateTransformTowards(Transform weapon, Vector3 to)
    {
        to.y = 0f;
        
        weapon.rotation = Quaternion.RotateTowards(
            weapon.rotation,
            Quaternion.LookRotation(to),
            Time.deltaTime * 175f
        );
    }
}
