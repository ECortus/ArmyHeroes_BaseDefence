using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallInfo : Detection
{
    public override float MaxHP 
    { 
        get
        {
            return InputHP * (1 + WallUpgradeInfo.Instance.Progress);
        }
    }

    [System.Serializable]
    public class Tier
    {
        public List<GameObject> Models = new List<GameObject>();
    }

    [SerializeField] private List<Tier> Tiers = new List<Tier>();

    void Start()
    {
        Heal(999f);
        /* GetHit(9999f); */

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

    int progress => WallUpgradeInfo.Instance.Progress;
    int index = -1;

    public void RefreshWallModel()
    {
        if(index != progress)
        {
            foreach(Tier tier in Tiers)
            {
                foreach (GameObject model in tier.Models)
                {
                    model.SetActive(false);
                }
            }
            
            index = progress;
        }

        int ind = Mathf.Clamp(progress, 0, Tiers.Count - 1);
        List<GameObject> models = Tiers[ind].Models;

        int t = (int)MaxHP / (models.Count - 1);
        int y = (int)HP;

        if(y <= 0f)
        {
            models[0].SetActive(true);
            y = 999999;
        }
        else
        {
            models[0].SetActive(false);
        }

        for(int i = 1; i < models.Count; i++)
        {
            y -= t;
            if(y <= 0)
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
