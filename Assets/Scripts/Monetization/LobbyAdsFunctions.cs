using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyAdsFunctions : MonoBehaviour
{
    [SerializeField] private GameObject noAdsObject, subObject;

    void OnEnable()
    {
        RefreshButtons();
    }
    
    public void RefreshButtons()
    {
        if(GameAds.Subcribed) subObject.SetActive(false);
        else subObject.SetActive(true);
        
        if(GameAds.NoAdsObjectBuyed) noAdsObject.SetActive(false);
        else noAdsObject.SetActive(true);
    }
    
    public void BuyNoAds()
    {
        GameAds.ActivateNoAds();
        RefreshButtons();
    }

    public void BuySub()
    {
        GameAds.ActivateSub();
        RefreshButtons();
    }

    public void CancelSub()
    {
        GameAds.DeactivateSub();
        RefreshButtons();
    }
}
