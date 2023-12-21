using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MAXHelper;
using Cysharp.Threading.Tasks;

public class BannerInit : MonoBehaviour
{
    private async void Start()
    {
        if (!GameAdsController.NoAds)
        {
            await UniTask.WaitUntil(() => AdsManager.Exist);
            AdsManager.ToggleBanner(true);
        }
    }

    private void OnDestroy()
    {
        if(AdsManager.Exist) AdsManager.ToggleBanner(false);
    }
}
