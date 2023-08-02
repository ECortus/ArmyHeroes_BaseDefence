using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirepositionUpgrader : UpgraderZone
{
    [SerializeField] private Fireposition fireposition;

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
        
        fireposition.Heal(999f);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }
}
