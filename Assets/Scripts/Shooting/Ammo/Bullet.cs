using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Ammo
{
    private float Damage => GetDamage();
    public override AmmoType Type => AmmoType.Bullet;

    public override void On(Vector3 spawn, Quaternion rot, Vector3 destination = new Vector3())
    {
        base.On(spawn, rot);

        rb.velocity = transform.forward * speed;
    }

    protected override void Update()
    {
        base.Update();
    }

    void OnTriggerEnter(Collider col)
    {
        GameObject go = col.gameObject;

        switch(go.tag)
        {
            case "Enemy":
                OnHit(col.GetComponent<Detection>());
                break;
            case "Ground":
                Off();
                break;
            case "Building":
                Off();
                break;
            default:
                break;
        }
    }
}   
