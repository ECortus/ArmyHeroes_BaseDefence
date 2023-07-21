using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearestDetector : BaseDetector
{
    Detection previous;
    float distanceToData;

    protected virtual void Reset() { }
    protected virtual void Change() { }
    protected virtual void Set() { }

    protected override IEnumerator Working()
    {
        while(true)
        {
            yield return null;

            if(data == null)
            {
                detection.NearestAround(priorityTypes, detectTypes, range, range, this, out data);
                previous = data;

                if(data != null)
                {
                    Set();
                }
            }

            if(data != null)
            {
                distanceToData = Vector3.Distance(data.transform.position, transform.position);

                if(!AdditionalCondition(data) || distanceToData > range)
                {
                    Reset();
                    continue;
                }

                if(detection.NearestAround(priorityTypes, detectTypes, range, distanceToData, this, out previous))
                {
                    if(data != previous)
                    {
                        data = previous;
                        Change();
                    }
                }
            }
        }
    }
}
