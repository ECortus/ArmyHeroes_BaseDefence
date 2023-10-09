using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FirepositionsOperatorInstaller : MonoInstaller
{
    [SerializeField] private FirepositionsOperator fpOperator;

    public override void InstallBindings()
    {
        Container.Bind<FirepositionsOperator>().FromInstance(fpOperator).AsSingle();
        Container.QueueForInject(fpOperator);
    }
}
