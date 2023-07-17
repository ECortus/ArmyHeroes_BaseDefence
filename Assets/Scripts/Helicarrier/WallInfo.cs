using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallInfo : Detection
{
    [SerializeField] private List<GameObject> models = new List<GameObject>();

    void Start()
    {
        Heal(999);
        Pool();
    }

    public override void Heal(float mnt)
    {
        base.Heal(mnt);
        RefreshWallModel();
    }

    public override void GetHit(float mnt)
    {
        base.GetHit(mnt);
        RefreshWallModel();
    }

    public void RefreshWallModel()
    {
        int t = (int)MaxHP / models.Count;
        int y = (int)HP;

        for(int i = 0; i < models.Count; i++)
        { 
            y -= t;
            if(y <= t)
            {
                models[i].SetActive(true);
                y = 999999;
            }
            else
            {
                models[i].SetActive(false);
            }
        }
    }
}
