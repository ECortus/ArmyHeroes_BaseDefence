using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviveUI : MonoBehaviour
{
    public static ReviveUI Instance { get; set; }
    void Awake() => Instance = this;

    [SerializeField] private OpenCloseObjectLevelUI objUI;
    [SerializeField] private Slider slider;

    [Space]
    [SerializeField] private OpenCloseObjectLevelUI nothanks;

    [Space]
    [SerializeField] private float delayTime = 3f;

    [Space] 
    [SerializeField] private GiveDynamitOnRevive dynamit;
    
    float time = 0f;

    bool Active = false;

    public void On()
    {
        nothanks.Close();
        objUI.Open();
        time = delayTime;

        slider.gameObject.SetActive(true);
        slider.minValue = 0f;
        slider.maxValue = delayTime;

        Active = true;
    }

    public void OnNoThanks()
    {
        slider.gameObject.SetActive(false);
        nothanks.Open();
        Active = false;
    }

    public async void Revive()
    {
        if (await GameAdsController.Instance.ShowRewardAd())
        {
            PlayerInfo.Instance.Resurrect();
            HelicarrierInfo.Instance.Resurrect();
            dynamit.Put();
        }
        else
        {
            NoThanks();
        }
        
        Off();
    }

    public void NoThanks()
    {
        InterstationalTimer.Instance.TryShowInterstationalAd();
        
        Off();
        UI.Instance.LoseLevel();
    }

    public void Off()
    {
        slider.gameObject.SetActive(false);
        nothanks.Close();
        objUI.Close();
    }

    void Update()
    {
        if(Active)
        {
            slider.value = time;
            time -= Time.unscaledDeltaTime;

            if(time <= 0f)
            {
                OnNoThanks();
            }
        }
    }
}
