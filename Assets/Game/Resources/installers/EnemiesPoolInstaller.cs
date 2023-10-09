using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemiesPoolInstaller : MonoInstaller
{
    [SerializeField] private EnemiesPool pool;

    public override void InstallBindings()
    {
        Container.Bind<EnemiesPool>().FromInstance(pool).AsSingle();
        Container.QueueForInject(pool);
    }
}
