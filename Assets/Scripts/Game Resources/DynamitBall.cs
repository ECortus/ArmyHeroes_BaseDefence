using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamitBall : ResourceBall
{
    [SerializeField] private float BLOWdistance;
    [SerializeField] private ParticleSystem dynamitBLOWEffect;

    protected override void AddRecourceToPlayer()
    {
        if(dynamitBLOWEffect != null) ParticlePool.Instance.Insert(ParticleType.Dynamit, dynamitBLOWEffect, transform.position);

        EnemiesPool.Instance.GetHitAllLowEnemies(transform.position, BLOWdistance, 999f);
        base.AddRecourceToPlayer();
    }
}
