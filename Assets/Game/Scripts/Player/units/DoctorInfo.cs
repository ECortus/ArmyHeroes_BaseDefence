using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class DoctorInfo : Detection
{
    [Space]
    [SerializeField] private Doctor doctor;
    [SerializeField] private DoctorDeathPool deathPool;

    public override async void Death()
    {
        base.Death();
        await UniTask.Delay(3000);

        gameObject.SetActive(false);
        deathPool.AddDoctor(doctor);
    }
}
