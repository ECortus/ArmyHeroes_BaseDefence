using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : BarUI
{
    [SerializeField] private Info info;
    
    protected override float Amount => info.Health;
    protected override float MaxAmount => info.MaxHealth;

    void Update()
    {
        transform.eulerAngles = Camera.main.transform.eulerAngles;
    }
}
