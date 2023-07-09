using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPool : MonoBehaviour
{
    public static AmmoPool Instance;
    void Awake() => Instance = this;

    private List<Ammo> BulletPool = new List<Ammo>();

    public Ammo Insert(AmmoType type, Ammo obj, Vector3 pos = new Vector3(), Quaternion rot = new Quaternion())
    {
        Ammo ammo = null;
        List<Ammo> list = new List<Ammo>();

        switch(type)
        {
            case AmmoType.Bullet:
                list = BulletPool;
                break;
            default:
                break;
        }

        if(list.Count > 0)
        {
            foreach(Ammo bllt in list)
            {
                if(bllt == null) continue;

                if(bllt.Hited)
                {
                    ammo = bllt;
                    ammo.On(pos, rot);
                    break;
                }
            }
        }
        
        if(ammo == null)
        {
            ammo = Instantiate(obj, pos, rot);
            ammo.On(pos, rot);

            AddBullet(type, ammo);
        }

        return ammo;
    }

    public void AddBullet(AmmoType type, Ammo bllt)
    {
        switch(type)
        {
            case AmmoType.Bullet:
                BulletPool.Add(bllt);
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
    Default, Bullet
}
