using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UltaActivatorUI : MonoBehaviour
{
    [SerializeField] private Ulta ulta;
    private bool Choised => UltaActivator.Instance.Ulta == ulta;

    private int Cost => ulta.Cost;
    private float Duration => ulta.Duration;

    [Space]
    public Button button;
    [SerializeField] private TextMeshProUGUI gemCounter;

    [Space]
    [SerializeField] private GameObject timer;
    [SerializeField] private TextMeshProUGUI timerCounter;

    public bool isActive => coroutine != null;
    Coroutine coroutine;

    void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClickAd()
    {
        //ad
        
        UltaActivator.Instance.SetUlta(ulta);
        UltaActivator.Instance.Activate();
        
        Reload();
    }

    public void OnButtonClick()
    {
        if (Statistics.Crystal >= Cost)
        {
            Crystal.Minus(Cost);
            UltaActivator.Instance.SetUlta(ulta);
            UltaActivator.Instance.Activate();
        
            Reload();
        }
    }

    public void Updating()
    {
        // if(!Choised)
        // {
        //     gameObject.SetActive(false);
        // }
        // else
        // {
        //     gameObject.SetActive(true);
        // }

        if(Statistics.Crystal < Cost)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }

        SetTextCost(true);
    }

    public void Reload()
    {   
        if(coroutine == null) StartCoroutine(Pause());
    }

    IEnumerator Pause()
    {
        float time = Duration;
        // button.interactable = false;
        UltaActivator.Instance.SetInteractableToAll(false);
        UltaActivator.Instance.SetTextToAll(false);
        
        timer.SetActive(true);

        while(time > 0f)
        {
            time -= Time.deltaTime;
            timerCounter.text = $"{(int)(time)}";

            yield return null;
        }

        timer.SetActive(false);
        coroutine = null;
        
        UltaActivator.Instance.SetInteractableToAll(true);
        UltaActivator.Instance.SetTextToAll(true);

        Updating();
        UltaActivator.Instance.Deactivate();
    }

    public void SetTextCost(bool state)
    {
        gemCounter.text = state ? $"{Cost}" : "---";
    }
}
