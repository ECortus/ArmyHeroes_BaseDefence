using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using TMPro;

public class GoldPopUp : MonoBehaviour
{
    [SerializeField] private float delay = 30f, waitDelay = 5f;
    [SerializeField] private int GoldPerWave = 75;
    private float time = 0f;

    [Space] [SerializeField] private Animation anim;
    [SerializeField] private Slider waitSlider;
    [SerializeField] private TextMeshProUGUI goldText;

    private int GoldReward
    {
        get
        {
            return GoldPerWave * (EndLevelStats.Instance.WaveIndex > 0 ? EndLevelStats.Instance.WaveIndex : 1);
        }
    }

    void Start()
    {
        time = delay;
    }
    
    void Update()
    {
        if (time > 0f && !GameAds.NoAds && GameManager.Instance.isActive)
        {
            time -= Time.deltaTime;

            if (time <= 0f)
            {
                ShowPopUp();
            }
        }
    }

    private Coroutine _coroutine;

    void ShowPopUp()
    {
        _coroutine ??= StartCoroutine(PopUp());
    }

    void ButtonClicked()
    {
        StopPopUp();
        OffSlider();
        
        Gold.Plus(GoldReward);
    }

    async void HidePopUp()
    {
        anim.Play("bounceSpawnReverse");

        float length = anim.GetClip("bounceSpawnReverse").length;
        await UniTask.Delay((int)(length * 1000));
        
        anim.gameObject.SetActive(false);
    }

    void StopPopUp()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    void OnSlider()
    {
        goldText.text = $"{GoldReward}";
        waitSlider.gameObject.SetActive(true);

        waitSlider.minValue = 0f;
        waitSlider.maxValue = waitDelay;
        waitSlider.value = 0f;
    }

    void RefreshSlider(float value)
    {
        waitSlider.value = value;
    }

    void OffSlider()
    {
        waitSlider.gameObject.SetActive(false);
    }

    IEnumerator PopUp()
    {
        anim.gameObject.SetActive(true);
        anim.Play("bounceSpawn");
        OnSlider();

        float time = waitDelay;

        while (time > 0f)
        {
            time -= Time.unscaledDeltaTime;
            RefreshSlider(time);
        }
        
        OffSlider();
        HidePopUp();
        
        yield return null;
    }
}
