using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : BarUI
{
    public Health data { get; set; }
    
    protected override float Amount => data.HP;
    protected override float MaxAmount => data.MaxHP;

    void Update()
    {
        transform.rotation = GameManager.Instance.Camera.rotation;
    }
}
