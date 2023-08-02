using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldProcessing : UltProcessing_IEnumerator
{
    [SerializeField] private UltShield info;
    [SerializeField] private EnergyShield shield;
    [SerializeField] private ParticleSystem effect;

    public override IEnumerator Process()
    {
        effect.Play();
        shield.On(info.ImpulseForce);
        yield return null;
    }

    public override IEnumerator Deprocess()
    {
        effect.Stop();
        shield.Off();
        yield return null;
    }
}
