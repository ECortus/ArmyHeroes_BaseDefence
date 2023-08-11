using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UltOrbHit", menuName = "Ulta/OrbHit")]
public class UltOrbHit : Ulta
{
    [Space]
    [SerializeField] private float damage = 100f;
    public float damageUpPerLVL = 50f;

    public float Range = 4f;
    public int ShootCount = 5;

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
            return damage + damageUpPerLVL * PowerLVL;
        }
    }

    public override void Activate()
    {
        UltProcessing.Instance.TurnOnOrbHit();
    }

    public override void Deactivate()
    {
        UltProcessing.Instance.TurnOffOrbHit();
    }
}
