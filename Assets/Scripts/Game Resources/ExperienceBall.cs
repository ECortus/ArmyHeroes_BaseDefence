using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceBall : ResourceBall
{
    [SerializeField] private ParticleSystem particle;
    
    public override void On(Vector3 pos = new Vector3())
    {
        base.On(pos);
        particle.Play();
    }

    public override void Off()
    {
        particle.Stop();
        base.Off();
    }

    protected override void AddRecourceToPlayer()
    {
        Experience.Plus(resourceAmount);
        base.AddRecourceToPlayer();
    }
}
