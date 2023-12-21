using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WavePassWindow : MonoBehaviour
{
    public static WavePassWindow Instance { get; set; }
    private void Awake() => Instance = this;

    [SerializeField] private OpenCloseObjectLevelUI toOn;
    [SerializeField] private TextMeshProUGUI killingText, coinText;

    [Space] 
    [SerializeField] private float timeToNoThanks;
    [SerializeField] private Slider slider;

    [Space] 
    [SerializeField] private GameObject noThanks;
    [SerializeField] private Transform rewardButton;

    private bool Open = false;

    private float time;

    private void Start()
    {
        Off();
    }

    public async void On()
    {
        SetGameTime(0f);
        
        time = timeToNoThanks;
        RefreshSlider();
        
        noThanks.SetActive(false);
        slider.gameObject.SetActive(false);
        
        toOn.Open();
        Open = true;

        rewardButton.transform.localScale = Vector3.zero;
        killingText.transform.localScale = Vector3.zero;
        coinText.transform.localScale = Vector3.zero;

        await UniTask.Delay(500, DelayType.UnscaledDeltaTime);
        
        killingText.text = EndLevelStats.Instance.KillingCount.ToString();
        killingText.transform.DOScale(1f, 0.25f).SetUpdate(true);

        await UniTask.Delay(500, DelayType.UnscaledDeltaTime);
        
        // coinText.text = ((int)LevelManager.Instance.ActualLevel.Chest.AllAmount).ToString();
        coinText.text = LevelManager.Instance.ActualLevel.GoldRewardPerWave.ToString();
        coinText.transform.DOScale(1f, 0.25f).SetUpdate(true);

        await UniTask.Delay(500, DelayType.UnscaledDeltaTime);
        
        rewardButton.DOScale(1f, 0.25f).SetUpdate(true);
        slider.gameObject.SetActive(true);
    }

    async void OpenNoThanks()
    {
        slider.gameObject.SetActive(false);
        noThanks.SetActive(true);
        noThanks.transform.localScale = Vector3.zero;

        noThanks.transform.DOScale(1f, 0.25f).SetUpdate(true);
    }

    public void Off()
    {
        SetGameTime(1f);
        
        Open = false;
        toOn.Close();
    }

    private void Update()
    {
        if (Open)
        {
            if (slider.gameObject.activeSelf)
            {
                time -= Time.unscaledDeltaTime;
                RefreshSlider();
            
                if (time < 0f)
                {
                    OpenNoThanks();
                }
            }
        }
    }

    void SetGameTime(float time)
    {
        Time.timeScale = time;
    }

    void RefreshSlider()
    {
        slider.minValue = 0f;
        slider.maxValue = timeToNoThanks;
        slider.value = timeToNoThanks - time;
    }

    public async void OnAdButtonClick()
    {
        if(await GameAdsController.Instance.ShowRewardAd()) LevelManager.Instance.ActualLevel.PlusGoldForWave();
        
        Off();
    }
}
