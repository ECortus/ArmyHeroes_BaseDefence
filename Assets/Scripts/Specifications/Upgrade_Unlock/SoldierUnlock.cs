using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierUnlock : UpgraderZone
{
    [Space]
    [SerializeField] private Soldier soldier;
    [SerializeField] private List<Transform> spawnDots = new List<Transform>();

    protected override bool ConditionToAllowInter 
    { 
        get
        {
            return Statistics.Gold > 0 && Player.Instance.direction == Vector3.zero 
                && (Input.touchCount == 0 && !Input.GetMouseButton(0));
        }
    }

    protected override void RecourceUse(int amount) 
    { 
        Gold.Minus(amount);
    }

    protected override void Complete() 
    {
        base.Complete();
        Spawn();
    }

    void SpawnOnStart()
    {
        for(int i = 0; i < Progress; i++)
        {
            Spawn();
        }
    }

    void Spawn()
    {   
        Vector3 pos = spawnDots[Random.Range(0, spawnDots.Count)].position;
        Quaternion rot = Quaternion.Euler(
            0f, Random.Range(0f, 360f), 0f
        );

        Soldier sold = Instantiate(soldier, pos, rot);
    }
}
