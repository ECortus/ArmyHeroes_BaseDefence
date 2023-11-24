using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Roulette : MonoBehaviour
{
    [SerializeField] private int defaultTokenCount, tokenCountUpPerRoll;

    public int TokenRequire
    {
        get
        {
            return defaultTokenCount + tokenCountUpPerRoll * RollCount;
        }
    }

    public int RollCount
    {
        get
        {
            return PlayerPrefs.GetInt(name + "ROULETTE", 0);
        }
        set
        {
            PlayerPrefs.SetInt(name + "ROULETTE", value);
            PlayerPrefs.Save();
        }
    }

    public void RefreshMainButton()
    {
        if(Statistics.Token < TokenRequire)
        {
            rouletteButton.interactable = false;
        }
        else
        {
            rouletteButton.interactable = true;
        }

        rouletteButtonText.text = $"{TokenRequire}";
    }

    [Space]
    [SerializeField] private float duration = 5f;
    [SerializeField] private float pauseBetweenEachRandom = 0.25f;

    [Space]
    [SerializeField] private Button rouletteButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI rouletteButtonText;
    [SerializeField] private GameObject getGameObject;

    [Space]
    [SerializeField] private UpgradeButtonUI[] Buttons;

    [Space]
    public Image choisedImage;
    public TextMeshProUGUI choisedText;

    int ChoisedIndex = -1;

    Coroutine coroutine;
    float time = 0f;

    void OnEnable()
    {
        RefreshMainButton();

        RefreshButtons();
        RefreshOutlines();
    }

    public void On()
    {
        if (TokenRequire < Statistics.Token) return;
        
        if(coroutine == null)
        {
            rouletteButton.interactable = false;
            coroutine = StartCoroutine(Rolling());
        }
    }

    IEnumerator Rolling()
    {
        time = duration;
        float mod = duration / 2f;

        closeButton.interactable = false;

        Token.Minus(TokenRequire);

        while(time > 0f)
        {
            ChoisedIndex = Random.Range(0, Buttons.Length);
            RefreshOutlines();

            time -= pauseBetweenEachRandom * 2f / mod;
            yield return new WaitForSeconds(pauseBetweenEachRandom / mod);

            if(mod > 1f)
            {
                mod -= pauseBetweenEachRandom;
            }
        }

        OnChoisedInfo();

        yield return new WaitUntil(() => !getGameObject.activeSelf);

        closeButton.interactable = true;
        Off();
    }

    public void ApplyOne()
    {
        Buttons[ChoisedIndex].Apply();

        OffChoisedInfo();
    }

    public void ApplyTwo()
    {
        Buttons[ChoisedIndex].Apply();
        Buttons[ChoisedIndex].Apply();

        RollCount++;

        OffChoisedInfo();
    }

    public void OnChoisedInfo()
    {
        getGameObject.SetActive(true);

        RollCount++;
        
        Buttons[ChoisedIndex].RefreshGetUI();
    }

    public void OffChoisedInfo()
    {
        RefreshMainButton();
        getGameObject.SetActive(false);
    }

    public void Off()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        ChoisedIndex = -1;
        RefreshOutlines();

        rouletteButton.interactable = true;
    }

    public void RefreshButtons()
    {
        for(int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].Refresh();
        }
    }

    public void RefreshOutlines()
    {
        for(int i = 0; i < Buttons.Length; i++)
        {
            if(i == ChoisedIndex)
            {
                Buttons[i].OnOutline();
            }
            else
            {
                Buttons[i].OffOutline();
            }
        }
    }
}
