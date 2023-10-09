using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using UnityEngine.Serialization;

public class RewardPopUpController : MonoBehaviour
{
    [SerializeField] private PopUpAction[] actions;
    [SerializeField] private GameObject bg;
    [SerializeField] private Animator anim;
    
    [Space]
    [SerializeField] private float delay = 30f;
    
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

    int ShowRandom()
    {
        bg.SetActive(true);
        int index = Random.Range(0, actions.Length);

        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].gameObject.SetActive(false);
        }
        
        actions[index].gameObject.SetActive(true);
        return index;
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
        
        time = delay;
    }
}
