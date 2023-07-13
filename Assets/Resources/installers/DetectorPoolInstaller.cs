using UnityEngine;
using Zenject;

public class DetectorPoolInstaller : MonoInstaller
{
    [SerializeField] private DetectorPool pool;

    public override void InstallBindings()
    {
        Container.Bind<DetectorPool>().FromInstance(pool).AsSingle();
        Container.QueueForInject(pool);
    }
}