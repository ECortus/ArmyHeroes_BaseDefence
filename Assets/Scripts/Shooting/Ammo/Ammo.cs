using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public virtual AmmoType Type { get; }
    protected virtual ParticleType Particle { get; }

    public Rigidbody rb;
    [SerializeField] private SphereCollider sphere;

    [Space]
    public float speed;

    [Space]
    [SerializeField] private TrailRenderer trial;
    [SerializeField] private GameObject destroyEffect;

    private float damage;
    public void SetDamage(float dmg)
    {
        damage = dmg;
    }
    public float GetDamage() => damage;

    public bool Active => gameObject.activeSelf;

    private SpecificAmmoEffect specific;
    public void SetSpecificEffect(SpecificAmmoEffect eff)
    {
        specific = eff;
    }   

    public virtual void On(Vector3 spawn, Quaternion rot, Vector3 destination = new Vector3())
    {
        trial.Clear();

        gameObject.SetActive(true);

        if(specific != null) specific.On();

        spawnPos = spawn;
        transform.position = spawn;
        transform.rotation = rot;
    }

    public virtual void Off()
    {
        gameObject.SetActive(false);
        EffectOnOff();

        if(specific != null) specific.Off();
    }   

    Vector3 spawnPos;

    public bool FarAwayFromSpawn
    {
        get
        {
            return Vector3.Distance(Center, spawnPos) > 100f;
        }
    }

    protected virtual void Update()
    {
        if(FarAwayFromSpawn)
        {
            Off();
        }

        transform.forward = rb.velocity.normalized;
    }

    public Vector3 Center
    {
        get
        {
            return transform.TransformPoint(sphere.center);
        }
    }

    public virtual void OnHit(Detection det)
    {
        det.GetHit(GetDamage());
        if(specific != null) specific.OnHit();
        
        Off();
    }

    void EffectOnOff()
    {
        if(destroyEffect != null) ParticlePool.Instance.Insert(Particle, destroyEffect, Center);
    }
}
