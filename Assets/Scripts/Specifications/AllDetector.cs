using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDetector : BaseDetector
{
    private Detection[] Detected;

    protected override IEnumerator Working()
    {
        while(true)
        {
            if(data == null)
            {
                if(detection.AllAround(priorityTypes, detectTypes, range, range, this, out Detected))
                {
                    data = Detected[0];
                    Set();
                }
            }

            yield return null;
        }
    }
}
