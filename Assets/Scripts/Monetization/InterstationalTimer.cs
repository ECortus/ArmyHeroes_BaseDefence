using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterstationalTimer : MonoBehaviour
{
    [SerializeField] private float delay = 30f;
    private float time = 0f;

    void Start()
    {
        time = delay;
    }
    
    void Update()
    {
        if (time > 0f)
        {
            time -= Time.deltaTime;
        }
    }

    public void TryShowInterstationalAd()
    {
        if (time < 0f && !GameAds.NoAds)
        {
            ///show
            time = delay;
        }
    }
}
