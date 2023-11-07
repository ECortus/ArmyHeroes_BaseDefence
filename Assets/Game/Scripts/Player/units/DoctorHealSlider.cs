using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorHealSlider : BarUI
{
    private Transform parentGrid => LevelManager.Instance.ActualLevel.HealTimersGridParent;
    [SerializeField] private DoctorDetector Detector;

    public void On()
    {
        transform.SetParent(parentGrid);
        
        transform.localPosition = new Vector3(transform.position.x, transform.position.y, 0);
        transform.localEulerAngles = Vector3.zero;
        
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
