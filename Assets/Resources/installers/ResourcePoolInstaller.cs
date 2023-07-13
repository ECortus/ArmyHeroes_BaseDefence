using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ResourcePoolInstaller : MonoInstaller
{
    [SerializeField] private ResourcePool pool;

    public override void InstallBindings()
    {
        Container.Bind<ResourcePool>().FromInstance(pool).AsSingle();
        Container.QueueForInject(pool);
    }
}
