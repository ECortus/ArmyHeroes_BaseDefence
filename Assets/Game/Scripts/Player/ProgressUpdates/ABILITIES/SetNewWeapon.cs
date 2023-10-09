using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPB_SetNewWeapon", menuName = "NPB-s/Set New Weapon")]
public class SetNewWeapon : NewProgressBonus
{
    [SerializeField] private int Index = 0;

    public override bool AdditionalContidion => PlayerInfo.Instance.WeaponIndex + 1 == Index;

    public override void Apply()
    {
        PlayerInfo.Instance.SetWeapon(Index);
    }

    public override void Cancel()
    {
        /* PlayerInfo.Instance.SetWeapon(0); */
    }
}
