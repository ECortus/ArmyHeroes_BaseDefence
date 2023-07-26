using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ElectricSpecific", menuName = "Specifics/Electric")]
public class ElectricAmmo : SpecificAmmoOnHitEffect
{
    [SerializeField] private float damage;
    [SerializeField] private int maxTargetsCount;
    [SerializeField] private float maxDistanceBetweenTargets;

    public override void Action(Detection det)
    {
        det.GetComponentInChildren<ProduceHitOfSpecificAmmo>().TurnOnElectric(damage, maxDistanceBetweenTargets, maxTargetsCount);
    }
}
