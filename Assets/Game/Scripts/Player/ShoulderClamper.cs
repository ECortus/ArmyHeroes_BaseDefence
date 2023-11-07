using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderClamper : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x,
            ClampAngle(transform.localEulerAngles.y, -85f, 85f),
            transform.localEulerAngles.z);
    }
    
    float ClampAngle(float angle, float from, float to)
    {
        if (angle < 0f) angle = 360 + angle;

        if (from < 0f) from += 360f;

        if (angle > 180f)
        {
            if (angle < from) angle = from;
        }
        else
        {
            if (angle > to) angle = to;
        }
        
        return angle;
    }
}
