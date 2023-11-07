using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class EnemyInfo : Detection
{
    [SerializeField] private float Damage;
    [SerializeField] private Animation bodyAnim;

    public void Attack(Detection nf)
    {
        if(nf != null) nf.GetHit(Damage);
    }

    public override void Resurrect()
    {
        base.Resurrect();
        bodyAnim.transform.localPosition = Vector3.zero;
    }

    public override async void Death()
    {
        base.Death();

        EndLevelStats.Instance.PlusKillingCount();
        GeneratorUI.Instance.UpdateSlider(1);

        await UniTask.Delay(3000);
        bodyAnim.Play();
        await UniTask.Delay(2000);

        gameObject.SetActive(false);
    }
}
