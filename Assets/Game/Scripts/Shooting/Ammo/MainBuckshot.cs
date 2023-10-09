using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBuckshot : Ammo
{
    private float Damage => GetDamage();
    public override AmmoType Type => AmmoType.MainBuckshot;

    [Space]
    [SerializeField] private SubBuckshot sub;
    [SerializeField] private int buckCount;
    [SerializeField] private float angleBetweenBucks;

    public override void On(Vector3 spawn, Quaternion rot, Vector3 destination = new Vector3())
    {
        gameObject.SetActive(true);

        Vector3 pos = spawn;
        Vector3 mainRot = rot.eulerAngles;

        Vector3 rotate = Vector3.zero;

        Ammo ammo = null;

        int mod = (buckCount - 1) / 2;

        mainRot -= new Vector3(
            0f,
            angleBetweenBucks * mod,
            0f
        );

        for(int i = 0; i < buckCount; i++)
        {
            rotate = mainRot + new Vector3(
                0f,
                angleBetweenBucks * i,
                0f
            );

            ammo = AmmoPool.Instance.Insert(sub.Type, sub);
            ammo.On(pos, Quaternion.Euler(rotate));
            ammo.SetDamage(Damage / 6f);
            ammo.SetSpecifics(Specifics);
        }

        gameObject.SetActive(false);
    }
}   
