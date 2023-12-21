using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyAdsFunctions : MonoBehaviour
{
    [SerializeField] private GameObject noAdsObject, subObject;

    void OnEnable()
    {
        if (GameAdsController.NoAds)
        {
            GameAdsController.Instance.ActivateNoAds();
            noAdsObject.SetActive(false);
            
            return;
        }
        
        GameAdsController.OnGameAdsUpdate += RefreshButtons;
        RefreshButtons();
    }

    private void OnDestroy()
    {
        GameAdsController.OnGameAdsUpdate -= RefreshButtons;
    }

    public void RefreshButtons()
    {
        // subObject.SetActive(!GameAdsController.Subscribed);
        noAdsObject.SetActive(!GameAdsController.NoAdsObjectBuyed);
    }
    
    public void BuyNoAds()
    {
        GameAdsController.Instance.ActivateNoAds();
    }

    public void BuySub()
    {
        GameAdsController.Instance.ActivateSub();
    }
}
