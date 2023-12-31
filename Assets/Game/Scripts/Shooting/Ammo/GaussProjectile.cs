using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaussProjectile : Ammo
{
    public override AmmoType Type => AmmoType.GaussProjectile;

    [Space]
    [SerializeField] private ParticleSystem projectile;
    [SerializeField] private ParticleSystem destroy;

    public override void On(Vector3 spawn, Quaternion rot, Vector3 destination = new Vector3())
    {
        base.On(spawn, rot);

        projectile.Play();
        rb.velocity = transform.forward * speed;
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Off(Transform pos = null)
    {
        projectile.Stop();
        if(destroy != null)
        {
            destroy.Play();
        }

        base.Off();
    }
}
