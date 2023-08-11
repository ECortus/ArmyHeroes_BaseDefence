using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTimeUpdater : MonoBehaviour
{
    private float time 
    {
        get => EndLevelStats.Instance.Time;
        set
        {
            EndLevelStats.Instance.Time = value;
        }
    }

    void Update()
    {
        time += Time.deltaTime;
    }
}
