using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Player player;

    public override void InstallBindings()
    {
        Container.Bind<Player>().FromInstance(player).AsSingle();
        Container.QueueForInject(player);
    }
}
