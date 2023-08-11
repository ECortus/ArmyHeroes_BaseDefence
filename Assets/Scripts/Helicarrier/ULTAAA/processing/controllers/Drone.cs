using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
public class Drone : Target
{
    [SerializeField] private float height = 4f;
    [SerializeField] private float moveSpeed, rotateSpeed;

    [Space]
    [SerializeField] private Transform body;
    [SerializeField] private RangeShootingDetector detector;
    [SerializeField] private GunHandler gunHandler;

    [Space]
    [SerializeField] private Transform[] gunBones;
    [SerializeField] private Transform[] muzzles;

    [Space]
    [SerializeField] private Animation anim;
    [SerializeField] private ParticleSystem particle;
 
    public bool Active => gameObject.activeSelf;

    public void On(Vector3 spawn)
    {
        gameObject.SetActive(true);
        transform.position = new Vector3(spawn.x, height, spawn.z);

        anim.Play();
        particle.Play();

        detector.On();
    }

    public async void Off()
    {
        detector.Off();
        gunHandler.Disable();

        SetTarget(Helicarrier.Instance.Transform);

        await UniTask.WaitUntil(() => (target.position - transform.position).magnitude < 0.5f);

        anim.Play("bounceSpawnReverse");
        await UniTask.Delay((int)(anim.GetClip("bounceSpawnReverse").length * 1000));

        gameObject.SetActive(false);
    }

    Vector3 dir;

    Vector3 angles;
    Quaternion targetRotation;

    Vector3 movePoint
    {
        get
        {
            return new Vector3(target.position.x, height, target.position.z);
        }
    }

    void Update()
    {   
        Move();
        Rotate();

        CorrectMuzzleRotation();
    }

    void Move()
    {
        if(Vector3.Distance(transform.position, movePoint) > 5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoint, moveSpeed * Time.deltaTime);
        }
    }

    void Rotate()
    {
        dir = (target.position - transform.position).normalized;

        targetRotation = Quaternion.LookRotation(dir);
        angles = targetRotation.eulerAngles;

        angles.x = Mathf.Clamp(angles.x, 0f, 30f);
        angles.z = 0f;

        body.rotation = Quaternion.RotateTowards(
            body.rotation, Quaternion.Euler(angles), Time.deltaTime * rotateSpeed
        );

        /* int i = 0;
        foreach(Transform bone in gunBones)
        {
            bone.rotation = muzzles[i].rotation;
            i++;
        } */
    }

    void CorrectMuzzleRotation()
    {
        foreach(Transform muzzle in muzzles)
        {
            muzzle.forward = (target.position + new Vector3(0f, 1f, 0f) - muzzle.position).normalized;
        }
    }
}
