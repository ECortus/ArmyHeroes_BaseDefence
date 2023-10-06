using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UltaPopUp : MonoBehaviour
{
    [SerializeField] private Animation[] buttonsAnims;
    
    [SerializeField] private float delay = 30f, waitDelay = 5f;
    private float time = 0f;

    [Space] [SerializeField] private Slider waitSlider;

    private int CurrentIndex = 0;

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

    int ShowRandom()
    {
        int index = Random.Range(0, buttonsAnims.Length);

        for (int i = 0; i < buttonsAnims.Length; i++)
        {
            if (i == index)
            {
                buttonsAnims[i].gameObject.SetActive(true);
                buttonsAnims[i].Play("bounceSpawn");
            }
            else
            {
                buttonsAnims[i].gameObject.SetActive(false);
            }
        }

        return index;
    }

    void ButtonClicked()
    {
        StopPopUp();
        
        OffSlider();
        for (int i = 0; i < buttonsAnims.Length; i++)
        {
            buttonsAnims[i].gameObject.SetActive(false);
        }
        
        UltaActivator.Instance.ActivateByIndex(CurrentIndex);
    }

    async void HidePopUp(int index)
    {
        Animation button = buttonsAnims[index];
        button.Play("bounceSpawnReverse");

        float length = button.GetClip("bounceSpawnReverse").length;
        await UniTask.Delay((int)(length * 1000));
        
        button.gameObject.SetActive(false);
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
        CurrentIndex = ShowRandom();
        OnSlider();

        float time = waitDelay;

        while (time > 0f)
        {
            time -= Time.unscaledDeltaTime;
            RefreshSlider(time);
        }
        
        OffSlider();
        HidePopUp(CurrentIndex);
        
        yield return null;
    }
}
