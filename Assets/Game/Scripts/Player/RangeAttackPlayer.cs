using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class RangeAttackPlayer : PlayerNearestDetector
{
    [Space] [SerializeField] private Player controller;
    [SerializeField] private GunHandler shooting;

    [Space] [SerializeField] private Transform leftShoulder;
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
            /*&& Vector3.Angle(transform.forward, (dt.transform.position - transform.position).normalized) < angle / 2f*/
            ;
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
        if (!shooting.isEnable)
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
        Vector3 direction, shoulderDirection;
        Transform firstWeapon = Weapons.First, secondWeapon = Weapons.Second, 
            firstSh = Shoulders.First, secondSh = Shoulders.Second;

        if (data != null)
        {
            if (addit_data != null)
            {
                controller.SetRotateDir(MiddleRotateDir());
                
                if (!DataBelongToFirst)
                {
                    firstWeapon = Weapons.Second;
                    secondWeapon = Weapons.First;
                    firstSh = Shoulders.Second;
                    secondSh = Shoulders.First;
                }
            }
            else
            {
                controller.ResetRotateDir();
            }

            direction = (data.transform.position - firstWeapon.position).normalized;
            shoulderDirection = (data.transform.position - firstSh.position).normalized;

            CorrectRotate((data.transform.position - transform.position).normalized, ref shoulderDirection);

            RotateTransformTowards(firstSh, shoulderDirection);
            RotateWeaponTowards(firstWeapon, direction);

            if (addit_data != null)
            {
                direction = (addit_data.transform.position - secondWeapon.position).normalized;
                shoulderDirection = (addit_data.transform.position - secondSh.position).normalized;

                CorrectRotate((addit_data.transform.position - transform.position).normalized, ref shoulderDirection);
                
                controller.SetRotateDir(MiddleRotateDir());
            }
            else
            {
                direction = (data.transform.position - secondWeapon.position).normalized;
                shoulderDirection = (data.transform.position - secondSh.position).normalized;

                CorrectRotate((data.transform.position - transform.position).normalized, ref shoulderDirection);
            }

            RotateTransformTowards(secondSh, shoulderDirection);
            RotateWeaponTowards(secondWeapon, direction);
        }
        else
        {
            direction = transform.forward;

            RotateTransformTowards(firstSh, direction);
            RotateTransformTowards(secondSh, direction);

            RotateWeaponTowards(Weapons.First, direction);
            RotateWeaponTowards(Weapons.Second, direction);
            
            controller.ResetRotateDir();
        }
    }

    bool DataBelongToFirst
    {
        get
        {
            Vector3 center = transform.position;
            Vector3 right = center + transform.right * 2f;
            Vector3 left = center - transform.right * 2f;

            Vector3 pos = data.transform.position;

            if (Vector3.Distance(pos, right) > Vector3.Distance(pos, left))
            {
                return true;
            }
            
            return false;
        }
    }

    void CorrectRotate(Vector3 dir, ref Vector3 shoulderDir)
    {
        if (Vector3.Angle(dir, shoulderDir) > Angle / 2f)
        {
            float angleInDegrees = Angle / 2f + Vector3.Angle(dir, Vector3.forward);
            shoulderDir = new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0,
                Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }

    Vector3 MiddleRotateDir()
    {
        Vector3 first = data.transform.position;
        Vector3 second = addit_data.transform.position;

        Vector3 middle = first + (first - second).normalized * Vector3.Distance(first, second) / 2f;
        middle += transform.right * 2f;

        Vector3 to = transform.position;

        return (middle - to).normalized;
    }

    private void RotateTransformTowards(Transform trans, Vector3 to)
    {
        to.y = 0f;
        trans.rotation = Quaternion.RotateTowards(trans.rotation, Quaternion.LookRotation(to), 360f * Time.deltaTime);
    }
    
    private void RotateWeaponTowards(Transform weapon, Vector3 to)
    {
        to.y = 0f;
        weapon.rotation = Quaternion.RotateTowards(weapon.rotation, Quaternion.LookRotation(to), 135f * Time.deltaTime);
    }
}

