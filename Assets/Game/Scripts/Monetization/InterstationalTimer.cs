using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterstationalTimer : MonoBehaviour
{
    public static InterstationalTimer Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private float delay = 30f;
    public static float Time { get; set; }

    [Space] [SerializeField] private RateUs rateUs;

    void Start()
    {
        Time = delay;
    }
    
    void Update()
    {
        if (Time > 0f && !GameAdsController.NoAds && Statistics.LevelIndex > 0)
        {
            Time -= UnityEngine.Time.unscaledTime;
        }
    }

    public void TryShowInterstationalAd()
    {
        if(rateUs) rateUs.TryOpen();
        
        if (Time < 0f && !GameAdsController.NoAds)
        {
            GameAdsController.Instance.ShowInterstationalAd();
            Time = delay;
        }
    }
}
