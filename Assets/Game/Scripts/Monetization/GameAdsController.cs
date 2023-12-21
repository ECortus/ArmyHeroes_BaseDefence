using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using MAXHelper;

public class GameAdsController : MonoBehaviour
{
    public static GameAdsController Instance { get; private set; }

    public static Action OnGameAdsUpdate;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static bool NoAds => NoAdsObjectBuyed || Subscribed;

    public static bool NoAdsObjectBuyed
    {
        get
        {
            return PlayerPrefs.GetInt("NoAds", 0) != 0;
        }
        set
        {
            PlayerPrefs.SetInt("NoAds", value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public void ActivateNoAds()
    {
        NoAdsObjectBuyed = true;
        OnGameAdsUpdate?.Invoke();
        
        AdsManager.CancelAllAds();
    }
    
    public void DeactivateNoAds()
    {
        NoAdsObjectBuyed = false;
        OnGameAdsUpdate?.Invoke();
    }
    
    public static bool Subscribed 
    {
        get
        {
            return PlayerPrefs.GetInt("NoAds", 0) != 0;
        }
        set
        {
            PlayerPrefs.SetInt("NoAds", value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public void ActivateSub()
    {
        Subscribed = true;
        OnGameAdsUpdate?.Invoke();

        AdsManager.CancelAllAds();
    }
    
    public void DeactivateSub()
    {
        Subscribed = false;
        OnGameAdsUpdate?.Invoke();
    }

    public async UniTask<bool> ShowRewardAd(string placement = "default")
    {
        // if (NoAds)
        // {
        //     return true;
        // }

        _adIsFinish = false;

        AdsManager.EResultCode Result = AdsManager.ShowRewarded(this.gameObject, OnFinishAds, placement);
        if (Result != AdsManager.EResultCode.OK) 
        {
            // здесь можно показать UI, что реклама не подгружена
            return false;
        }

        await UniTask.WaitUntil(() => _adIsFinish);
        return _success;
    }

    private bool _adIsFinish;
    private bool _success;
    
    private void OnFinishAds(bool success)
    {
        _adIsFinish = true;
        _success = success;
    }

    
    public void ShowInterstationalAd(string Placement = "default")
    {
        var Result = AdsManager.ShowInter(this.gameObject, OnInterDismissed, Placement);
        if (Result != AdsManager.EResultCode.OK) 
        {
            OnInterDismissed(false);
        }
    }
    
    private void OnInterDismissed(bool Success) 
    {
        // Success после закрытия интера всегда будет true. Но если вам важно,
        // показался интер или нет, обработайте случай, когда реклама из-за 
        // ошибки и не начала показ 
    }

}
