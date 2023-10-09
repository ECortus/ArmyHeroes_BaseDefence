using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ImpulseSpecific", menuName = "Specifics/Impulse")]
public class ImpulseAmmo : SpecificAmmoOnHitEffect
{
    [SerializeField] private float force;
        
    public override void Action(Detection det)
    {
        det.GetComponentInChildren<ProduceHitOfSpecificAmmo>().TurnOnImpulse(force);
    }
}
