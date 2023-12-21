using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public abstract class PlayerNearestDetector : BaseDetector
{
    public float Angle;
    
    public bool Addit_Data_Condition(Detection addit) => AdditionalCondition(addit)
        && Vector3.Angle((data.transform.position - transform.position).normalized, (addit.transform.position - transform.position).normalized) <= Angle / 2f;
    
    public Detection addit_data;

    private Detection[] Detections;
    
    Detection previous;
    float distanceToData;

    void SetAdditData()
    {
        foreach(Detection det in Detections)
        {
            if(det == data) continue;

            if (Vector3.Angle(transform.forward, (det.transform.position - transform.position).normalized) <= Angle / 2f)
            {
                addit_data = det;
                break;
            }
        }
    }

    protected override IEnumerator Working()
    {
        while(true)
        {
            yield return null;

            if(data == null)
            {
                if (addit_data && AdditionalCondition(addit_data))
                {
                    data = addit_data;
                    addit_data = null;
                    
                    Set();
                    continue;
                }
                
                if (detection.AllAround(priorityTypes, detectTypes, range, range, this, out Detections))
                {
                    data = Detections[0];
                    previous = data;

                    if (Detections.Length > 1)
                    {
                        SetAdditData();
                    }

                    Set();
                }
            }

            if(data)
            {
                if (addit_data)
                {
                    if (!Addit_Data_Condition(addit_data) || Vector3.Distance(addit_data.transform.position, transform.position) > range)
                    {
                        addit_data = null;
                    }
                }
                
                distanceToData = Vector3.Distance(data.transform.position, transform.position);

                if((!addit_data && !AdditionalCondition(data)) || 
                   (addit_data && !AdditionalCondition(data) && AdditionalCondition(addit_data)) 
                   || distanceToData > range)
                {
                    InColWithTargetMask = false;

                    if (addit_data && AdditionalCondition(addit_data))
                    {
                        data = addit_data;
                        addit_data = null;
                    
                        Set();
                        continue;
                    }

                    Reset();
                    continue;
                }

                if(detection.AllAround(priorityTypes, detectTypes, range, distanceToData, this, out Detections))
                {
                    previous = Detections[0];
                    
                    if(data != previous)
                    {
                        data = previous;
                        Change();
                    }
                    
                    if (Detections.Length > 1)
                    {
                        SetAdditData();
                    }
                }
            }
        }
    }
}
