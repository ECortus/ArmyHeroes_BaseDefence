using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallUpgradeInfo : UpgraderZone
{
    public static WallUpgradeInfo Instance { get; set; }
    void Awake() => Instance = this;

    private Detection[] wallsPool => DetectionPool.Instance.RequirePools(DetectType.Wall);

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

        foreach(Detection wall in wallsPool)
        {   
            wall.GetComponent<WallInfo>().RefreshWallModel();
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }
}
