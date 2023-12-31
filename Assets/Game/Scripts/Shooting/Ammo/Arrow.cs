using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Ammo
{
    public override AmmoType Type => AmmoType.Arrow;

    public override void On(Vector3 spawn, Quaternion rot, Vector3 destination = new Vector3())
    {
        base.On(spawn, rot);

        rb.velocity = transform.forward * speed;
    }

    protected override void Update()
    {
        base.Update();
    }
}
