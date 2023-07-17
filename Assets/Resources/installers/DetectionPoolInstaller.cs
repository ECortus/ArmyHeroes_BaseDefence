using UnityEngine;
using Zenject;

public class DetectionPoolInstaller : MonoInstaller
{
    [SerializeField] private DetectionPool pool;

    public override void InstallBindings()
    {
        Container.Bind<DetectionPool>().FromInstance(pool).AsSingle();
        Container.QueueForInject(pool);
    }
}