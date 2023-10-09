using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldProcessing : UltProcessing_IEnumerator
{
    [SerializeField] private UltShield info;
    [SerializeField] private EnergyShield shield;

    [Space]
    [SerializeField] private Animation anim;
    [SerializeField] private ParticleSystem effect, effectgen;

    public override IEnumerator Process()
    {
        effect.Play();
        effectgen.Play();
        anim.Play("bounceSpawn");

        shield.On(info.ImpulseForce);
        yield return null;
    }

    public override IEnumerator Deprocess()
    {
        anim.Play("bounceSpawnReverse");

        effect.Stop();
        effectgen.Stop();

        shield.Off();
        yield return null;
    }
}
