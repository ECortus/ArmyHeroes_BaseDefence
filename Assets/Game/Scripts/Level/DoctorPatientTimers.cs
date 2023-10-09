using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class DoctorPatientTimers : MonoBehaviour
{
    public bool AllHealed { get; set; }

    [SerializeField] private Transform grid;
    [SerializeField] private GameObject zone;

    bool AllOff
    {
        get
        {
            bool off = true;

            foreach (Transform VARIABLE in grid)
            {
                if (VARIABLE.gameObject.activeSelf)
                {
                    off = false;
                    break;
                }
            }
            
            return off;
        }
    }

    public void RefreshHealAllZone()
    {
        if (AllOff)
        {
            zone.SetActive(false);
        }
        else
        {
            zone.SetActive(true);
        }
    }
    
    public async void HealAll()
    {
        AllHealed = true;
        await UniTask.WaitUntil(() => AllOff);
        AllHealed = false;
        
        RefreshHealAllZone();
    }
}
