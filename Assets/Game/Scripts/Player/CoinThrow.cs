using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinThrow : MonoBehaviour
{
    public static CoinThrow Instance { get; set; }
    void Awake() => Instance = this;

    [SerializeField] private ParticleSystem particle;

    Transform target = null;

    Vector3 dir;

    public void On(Transform trg)
    {
        if(trg != null)
        {
            target = trg;
            particle.Play();
        }
    }

    public void Off()
    {
        particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        target = null;
    }

    void Update()
    {
        if(target != null)
        {
            dir = (target.position - transform.position).normalized;
            dir.y = 0f;

            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}
