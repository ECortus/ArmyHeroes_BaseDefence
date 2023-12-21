using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProgressiveTokenReward : MonoBehaviour
{
    [SerializeField] private OpenCloseObjectLevelUI objUI;
    [SerializeField] private Slider slider;

    [Space]
    [SerializeField] private OpenCloseObjectLevelUI nothanks;

    [Space]
    [SerializeField] private float delayTime = 3f;
    float time = 0f;

    [Space] [SerializeField] private UnityEvent loadEvent;

    bool Active = false;

    [Space] [SerializeField] private TextMeshProUGUI progressiveRewardText;

    private int ClickedLevelIndex
    {
        get
        {
            return PlayerPrefs.GetInt("ClickedProgressiveIndex", 0);
        }
        set
        {
            PlayerPrefs.SetInt("ClickedProgressiveIndex", value);
            PlayerPrefs.Save();
        }
    }
    
    private int CurrentLevelIndex
    {
        get
        {
            return Statistics.LevelIndex;
        }
    }

    private int ProgressiveIndex => Mathf.Clamp(CurrentLevelIndex - ClickedLevelIndex, 0, 2);

    private int ProgressiveMultiplier
    {
        get
        {
            int i = 1;

            switch(ProgressiveIndex)
            {
                case 0:
                    i = 3;
                    break;
                case 1:
                    i = 5;
                    break;
                case 2:
                    i = 7;
                    break;
                default:
                    break;
            }
            
            return i;
        }
    }

    public void On()
    {
        progressiveRewardText.text = $"X{ProgressiveMultiplier}";
        
        objUI.Open();
        time = delayTime;

        slider.gameObject.SetActive(true);
        slider.minValue = 0f;
        slider.maxValue = delayTime;

        Active = true;
    }

    public async void RewardButtonClick()
    {
        int mult = ProgressiveMultiplier;

        if (await GameAdsController.Instance.ShowRewardAd())
        {
            for (int i = 0; i < mult; i++)
            {
                EndLevelStats.Instance.GiveReward();
            }
        }

        ClickedLevelIndex = CurrentLevelIndex;
        Load();
    }

    public void NoThanksButtonClick()
    {
        EndLevelStats.Instance.GiveReward();
        Load();
    }

    public void Load()
    {
        loadEvent?.Invoke();
    }

    public void OnNoThanks()
    {
        slider.gameObject.SetActive(false);
        nothanks.Open();
        Active = false;
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
