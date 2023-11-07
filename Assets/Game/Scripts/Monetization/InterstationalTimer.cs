using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterstationalTimer : MonoBehaviour
{
    [SerializeField] private float delay = 30f;
    private float time = 0f;

    [Space] [SerializeField] private RateUs rateUs;

    void Start()
    {
        time = delay;
    }
    
    void Update()
    {
        if (time > 0f && !GameAds.NoAds && Statistics.LevelIndex > 0)
        {
            time -= Time.unscaledTime;
        }
    }

    public void TryShowInterstationalAd()
    {
        rateUs.TryOpen();
        
        if (time < 0f && !GameAds.NoAds)
        {
            ///show
            time = delay;
        }
    }
}
