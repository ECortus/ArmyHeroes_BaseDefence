using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllHealZone : BarUI
{
    [SerializeField] private float delay = 2f;
    private float time = 0f;

    protected override float Amount => time;
    protected override float MaxAmount => delay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            time = 0f;
            Refresh();
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            time += Time.deltaTime;
            Refresh();

            if (time >= delay)
            {
                LevelManager.Instance.ActualLevel.PatientTimers.HealAll();
                
                time = 0f;
                Refresh();
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            time = 0f;
            Refresh();
        }
    }
}
