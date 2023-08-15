using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class EnemyInfo : Detection
{
    [SerializeField] private float Damage;

    public void Attack(Detection nf)
    {
        if(nf != null) nf.GetHit(Damage);
    }

    public override async void Death()
    {
        base.Death();

        EndLevelStats.Instance.PlusKillingCount();
        await UniTask.Delay(3000);

        gameObject.SetActive(false);
    }
}
