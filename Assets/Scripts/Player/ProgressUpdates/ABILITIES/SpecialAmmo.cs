using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPB_SpecialAmmo", menuName = "NPB-s/Special Ammo")]
public class SpecialAmmo : NewProgressBonus
{
    [SerializeField] private SpecificAmmoEffect effect;

    public override void Apply()
    {

    }

    public override void Cancel()
    {
        
    }
}
