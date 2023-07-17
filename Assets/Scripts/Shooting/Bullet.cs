using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Ammo
{
    [SerializeField] private GameObject destroyEffect;
    private float Damage => GetDamage();

    protected override void EffectOnOff()
    {
        if(destroyEffect != null) ParticlePool.Instance.Insert(ParticleType.Bullet, destroyEffect, transform.position);
    }

    protected override void OnTriggerEnter(Collider col)
    {
        GameObject go = col.gameObject;

        switch(go.tag)
        {
            case "Enemy":
                col.GetComponent<Detection>().GetHit(Damage);
                Off();
                break;
            default:
                break;
        }
    }
}   
