using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerNearestDetector : BaseDetector
{
    public float Angle => 170f;
    
    public bool Addit_Data_Condition(Detection addit) => AdditionalCondition(addit)
        && Vector3.Angle(transform.forward, (addit.transform.position - transform.position).normalized) <= Angle / 2f;
    
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

            if(data != null)
            {
                if (addit_data != null)
                {
                    if (!Addit_Data_Condition(addit_data) || Vector3.Distance(addit_data.transform.position, transform.position) > range)
                    {
                        addit_data = null;
                    }
                    else if (!Addit_Data_Condition(data))
                    {
                        data = addit_data;
                        addit_data = null;
                    }
                }
                
                distanceToData = Vector3.Distance(data.transform.position, transform.position);

                if(!AdditionalCondition(data) || distanceToData > range)
                {
                    InColWithTargetMask = false;

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
