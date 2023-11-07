using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class RewardPopUpController : MonoBehaviour
{
    public static RewardPopUpController Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private PopUpAction[] actions;
    [SerializeField] private GameObject bg;
    [SerializeField] private Animator anim;
    
    [Space]
    [SerializeField] private float delay = 30f;

    [Space] [SerializeField] private PopUpAction MechAction;
    
    private float time = 0f;

    void Start()
    {
        time = delay;
    }
    
    void Update()
    {
        if (time > 0f && !GameAds.NoAds && GameManager.Instance.isActive && Statistics.LevelIndex > 0 && Time.timeScale > 0)
        {
            time -= Time.deltaTime;

            if (time <= 0f)
            {
                ShowPopUp();
            }
        }
    }

    void ShowPopUp()
    {
        ShowRandom();
        anim.SetTrigger("Show");

        Time.timeScale = 0f;
    }

    void ShowRandom()
    {
         Show(Random.Range(0, actions.Length));
    }

    void Show(int index)
    {
        bg.SetActive(true);

        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].gameObject.SetActive(false);
        }
        
        actions[index].gameObject.SetActive(true);
    }
    

    public void ShowMechPopUp()
    {
        bg.SetActive(true);

        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].gameObject.SetActive(false);
        }
        
        MechAction.gameObject.SetActive(true);
        
        anim.SetTrigger("Show");

        Time.timeScale = 0f;
    }

    public void OnButtonClick(PopUpAction action)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].gameObject.SetActive(false);
        }
        
        action.Do();
        ClosePopUp();
    }

    public async void ClosePopUp()
    {
        bg.SetActive(false);
        anim.SetTrigger("Hide");
        
        Time.timeScale = 1f;

        await UniTask.Delay(3000, DelayType.UnscaledDeltaTime);
        
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].gameObject.SetActive(false);
        }
        MechAction.gameObject.SetActive(false);
        
        time = delay;
    }
}
