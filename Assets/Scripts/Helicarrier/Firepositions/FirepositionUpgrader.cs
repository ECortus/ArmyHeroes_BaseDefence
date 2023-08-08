using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirepositionUpgrader : UpgraderZone
{
    [Space]
    [SerializeField] private Fireposition fireposition;
    private GameObject firepositionObject => fireposition.gameObject;

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

        if(Progress > -1)
        {
            firepositionObject.SetActive(true);
            fireposition.Heal(999f);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        if(Progress > -1)
        {
            firepositionObject.SetActive(true);
            fireposition.Heal(999f);
        }
        else
        {
            firepositionObject.SetActive(false);
        }
    }
}
