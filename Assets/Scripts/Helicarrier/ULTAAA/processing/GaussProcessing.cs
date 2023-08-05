using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaussProcessing : UltProcessing_IEnumerator
{   
    [SerializeField] private UltGauss info;

    [Space]
    [SerializeField] private GameObject obj;
    [SerializeField] private Gun[] guns;
    [SerializeField] private GunHandler gunHandler;
    [SerializeField] private RangeShootingDetector gaussDetector;

    public override IEnumerator Process()
    {
        obj.SetActive(true);

        foreach(var item in guns)
        {
            item.SetDefaultDamage(info.Damage);
        }

        gaussDetector.On();

        yield return null;
    }

    public override IEnumerator Deprocess()
    {
        gaussDetector.Off();
        gunHandler.Disable();

        obj.SetActive(false);

        yield return null;
    }
}
