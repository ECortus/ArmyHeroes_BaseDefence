using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorUnlock : UpgraderZone
{
    [Space]
    [SerializeField] private HumanoidController human;
    [SerializeField] private List<Transform> spawnDots = new List<Transform>();

    /*[Space] [SerializeField] private GameObject unbuilded;
    [SerializeField] private Animation builded;*/

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

        if (Progress == 1)
        {
            ChangeBuilding();
        }
    }

    protected override void OnEnable()
    {
        if (Statistics.LevelIndex > 0) SpawnOnStart();
        else Progress = 0;
        
        base.OnEnable();
    }

    void SpawnOnStart()
    {
        ChangeBuilding();
        
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

    void ChangeBuilding()
    {
        /*if (Progress > 0)
        {
            unbuilded.SetActive(false);
            builded.Play();
        }
        else
        {
            unbuilded.SetActive(true);
            builded.transform.localScale = Vector3.zero;
        }*/
    }
}
