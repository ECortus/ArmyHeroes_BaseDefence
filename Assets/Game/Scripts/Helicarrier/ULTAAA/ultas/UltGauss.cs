using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UltGauss", menuName = "Ulta/Gauss")]
public class UltGauss : Ulta
{
    [Space]
    [SerializeField] private float damage = 15f;
    public float damageUpPerLVL = 10f;

    public float DamageMod
    {
        get
        {
            return damageUpPerLVL * PowerLVL;
        }
    }

    public float Damage
    {
        get
        {
            return damage + DamageMod;
        }
    }

    public override void Activate()
    {
        UltProcessing.Instance.TurnOnGauss();
    }

    public override void Deactivate()
    {
        UltProcessing.Instance.TurnOffGauss();
    }
}
