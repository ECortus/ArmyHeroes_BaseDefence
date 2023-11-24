using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class HealthBarUI : BarUI
{
    public Health data { get; set; }
    [SerializeField] private RotationConstraint rot;
    
    protected override float Amount => data.HP;
    protected override float MaxAmount => data.MaxHP;

    void OnEnable()
    {
        if (!rot && !TryGetComponent(out rot))
        {
            rot = gameObject.AddComponent<RotationConstraint>();
        }
        
        ConstraintSource source = new ConstraintSource();
        source.sourceTransform = Camera.main.transform;
        source.weight = 1;
        rot.SetSources(new List<ConstraintSource>() {source});
        rot.locked = true;
    }

    /*void Update()
    {
        transform.rotation = GameManager.Instance.Camera.rotation;
    }*/
}
