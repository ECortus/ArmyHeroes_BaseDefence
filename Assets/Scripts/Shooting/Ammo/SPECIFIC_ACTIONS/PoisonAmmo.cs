using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PoisonSpecific", menuName = "Specifics/Poison")]
public class PoisonAmmo : SpecificAmmoOnHitEffect
{
    [SerializeField] private float damage, time;

    public override void Action(Detection det)
    {
        det.GetComponentInChildren<ProduceHitOfSpecificAmmo>().TurnOnPoison(damage, time);
    }
}
