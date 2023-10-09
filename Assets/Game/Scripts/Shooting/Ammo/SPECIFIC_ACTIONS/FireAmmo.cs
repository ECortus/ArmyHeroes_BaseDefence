using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireSpecific", menuName = "Specifics/Fire")]
public class FireAmmo : SpecificAmmoOnHitEffect
{
    [SerializeField] private float damage, time;

    public override void Action(Detection det)
    {
        det.GetComponentInChildren<ProduceHitOfSpecificAmmo>().TurnOnFire(damage, time);
    }
}
