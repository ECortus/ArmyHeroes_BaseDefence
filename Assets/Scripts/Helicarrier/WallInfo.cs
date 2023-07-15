using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallInfo : Info
{
    [Header("Info: ")]
    [SerializeField] private float health;
    [SerializeField] private List<GameObject> models = new List<GameObject>();

    public override float InputMaxHealth => health;
    public override float InputDamage => 0f;

    void Start()
    {
        Heal(999f);
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

    void RefreshWallModel()
    {
        int t = (int)MaxHealth / models.Count;
        int y = (int)Health / t;
        y = y < 0 ? 0 : y;
        y++;

        for(int i = 0; i < models.Count; i++)
        {   
            if(i == y)
            {
                models[i].SetActive(true);
            }
            else
            {
                models[i].SetActive(false);
            }
        }
    }
}
