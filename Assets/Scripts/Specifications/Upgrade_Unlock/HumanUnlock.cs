using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanUnlock : UpgraderZone
{
    [Space]
    [SerializeField] private HumanoidController human;
    [SerializeField] private List<Transform> spawnDots = new List<Transform>();

    protected override bool ConditionToAllowInter 
    { 
        get
        {
            return Statistics.Gold > 0 && Player.Instance.Direction == Vector3.zero 
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

    protected override void OnEnable()
    {
        SpawnOnStart();
        base.OnEnable();
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

        HumanoidController hmn = Instantiate(human);
        hmn.On(pos, rot);
    }
}
