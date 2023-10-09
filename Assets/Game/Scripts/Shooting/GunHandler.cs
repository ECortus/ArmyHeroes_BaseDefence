using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHandler : MonoBehaviour
{
    [System.Serializable]
    public class GunComplex
    {
        public List<GunPair> Pair = new List<GunPair>();
    }

    public GunComplex CurrentComplex => Guns[Index];

    [System.Serializable]
    public class GunPair
    {
        public Gun Gun;
        public Transform Point;
    }

    public int Index { get; set; }

    [SerializeField] private List<GunComplex> Guns = new List<GunComplex>();

    public Target Target;
    public HP_DMG_SPD HP_DMG_SPD;
    public ShootingUpgrades ShootingUpgrades;

    public int GunsCount => Guns[Index].Pair.Count;

    public void SetGunPair(int i)
    {
        Index = i;
        Refresh();
    }

    public bool isEnable
    {
        get
        {
            bool isenable = true;

            List<GunPair> list = Guns[Index].Pair;
            foreach(GunPair pair in list)
            {
                if(!pair.Gun.isEnable)
                {
                    isenable = false;
                    break;
                }
            }

            return isenable;
        }
    }

    public void Enable()
    {
        List<GunPair> list = Guns[Index].Pair;
        foreach(GunPair pair in list)
        {
            pair.Gun.Enable();
        }
    }

    public void Disable()
    {
        List<GunPair> list = Guns[Index].Pair;
        foreach(GunPair pair in list)
        {
            if(!pair.Gun.AutoShooting)
            {
                pair.Gun.Disable();
            }
        }
    }

    void Refresh()
    {
        bool someonewasenable = false;

        List<GunPair> list = Guns[Index].Pair;
        foreach(GunPair pair in list)
        {
            if(pair.Gun.isEnable)
            {
                someonewasenable = true;
                break;
            }
        }

        foreach(GunComplex complex in Guns)
        {
            foreach (GunPair pair in complex.Pair)
            {
                pair.Gun.Disable();
                pair.Gun.gameObject.SetActive(false);
            }
        }

        for(int i = 0; i < Guns.Count; i++)
        {
            if(i == Index)
            {
                list = Guns[i].Pair;
                foreach(GunPair pair in list)
                {
                    pair.Gun.Transform.parent = pair.Point;
                    pair.Gun.gameObject.SetActive(true);

                    pair.Gun.Transform.localPosition = Vector3.zero;
                }
            }
        }

        if(someonewasenable)
        {
            Enable();
        }
    }
}
