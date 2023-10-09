using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPool : MonoBehaviour
{
    public static AmmoPool Instance;
    void Awake() => Instance = this;

    private List<Ammo> 
        BulletPool = new List<Ammo>(),
        RocketPool = new List<Ammo>(),
        MainBuckshotPool = new List<Ammo>(),
        SubBuckshotPool = new List<Ammo>(),
        ArrowPool = new List<Ammo>(),
        GaussProjectilePool = new List<Ammo>(),
        FlamePool = new List<Ammo>();

    public Ammo Insert(AmmoType type, Ammo obj)
    {
        Ammo ammo = null;
        List<Ammo> list = new List<Ammo>();

        switch(type)
        {
            case AmmoType.Bullet:
                list = BulletPool;
                break;
            case AmmoType.Rocket:
                list = RocketPool;
                break;
            case AmmoType.MainBuckshot:
                list = MainBuckshotPool;
                break;
            case AmmoType.SubBuckshot:
                list = SubBuckshotPool;
                break;
            case AmmoType.Arrow:
                list = ArrowPool;
                break;
            case AmmoType.GaussProjectile:
                list = GaussProjectilePool;
                break;
            case AmmoType.Flame:
                list = FlamePool;
                break;
            default:
                break;
        }

        if(list.Count > 0)
        {
            foreach(Ammo bllt in list)
            {
                if(bllt == null) continue;

                if(!bllt.Active)
                {
                    ammo = bllt;
                    break;
                }
            }
        }
        
        if(ammo == null)
        {
            ammo = Instantiate(obj);
            AddAmmo(type, ammo);
        }

        return ammo;
    }

    public void AddAmmo(AmmoType type, Ammo bllt)
    {
        switch(type)
        {
            case AmmoType.Bullet:
                BulletPool.Add(bllt);
                break;
            case AmmoType.Rocket:
                RocketPool.Add(bllt);;
                break;
            case AmmoType.MainBuckshot:
                MainBuckshotPool.Add(bllt);
                break;
            case AmmoType.SubBuckshot:
                SubBuckshotPool.Add(bllt);
                break;
            case AmmoType.Arrow:
                ArrowPool.Add(bllt);
                break;
            case AmmoType.GaussProjectile:
                GaussProjectilePool.Add(bllt);
                break;
            case AmmoType.Flame:
                FlamePool.Add(bllt);
                break;
            default:
                break;
        }
    }

    public void ResetPool()
    {
        BulletPool.Clear();
    }
}

[System.Serializable]
public enum AmmoType
{
    Default, Bullet, MainBuckshot, SubBuckshot, Arrow, Rocket, GaussProjectile, Flame
}
