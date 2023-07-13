using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AmmoPoolInstaller : MonoInstaller
{
    [SerializeField] private AmmoPool pool;

    public override void InstallBindings()
    {
        Container.Bind<AmmoPool>().FromInstance(pool).AsSingle();
        Container.QueueForInject(pool);
    }
}
