using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDetector : BaseDetector
{
    private List<Detection> Detected;

    protected override IEnumerator Working()
    {
        while(true)
        {
            if(data == null)
            {
                if(detection.AllAround(priorityTypes, detectTypes, range, range, this, out Detected))
                {
                    data = Detected[Random.Range(0, Detected.Count)];
                }
            }

            yield return null;
        }
    }
}
