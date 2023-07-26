using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FreezeSpecific", menuName = "Specifics/Freeze")]
public class FreezeAmmo : SpecificAmmoOnHitEffect
{
    [SerializeField] private float decreaseOnPercent, time;

    public override void Action(Detection det)
    {
        det.GetComponentInChildren<ProduceHitOfSpecificAmmo>().TurnOnFreeze(decreaseOnPercent, time);
    }
}
