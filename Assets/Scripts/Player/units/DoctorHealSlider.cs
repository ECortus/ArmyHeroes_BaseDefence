using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorHealSlider : BarUI
{
    [SerializeField] private Transform parentGrid;
    [SerializeField] private DoctorDetector Detector;

    public void On()
    {
        transform.parent = parentGrid;
        
        gameObject.SetActive(true);
        Refresh();
    }

    public void Off()
    {
        gameObject.SetActive(false);
    }

    protected override float Amount => Detector.MaxHealTime - Detector.CurrentHealTime;
    protected override float MaxAmount => Detector.MaxHealTime;
}
