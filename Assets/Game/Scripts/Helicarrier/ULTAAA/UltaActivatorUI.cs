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
    [SerializeField] private Button button;
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
        
        UltaActivator.Instance.Activate();
        Reload();
    }

    public void OnButtonClick()
    {
        Crystal.Minus(Cost);
        UltaActivator.Instance.Activate();
        
        Reload();
    }

    public void Updating()
    {
        if(!Choised)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }

        if(Statistics.Crystal < Cost)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }

        gemCounter.text = $"{Cost}";
    }

    public void Reload()
    {   
        if(coroutine == null) StartCoroutine(Pause());
    }

    IEnumerator Pause()
    {
        float time = Duration;
        button.interactable = false;

        gemCounter.text = $"---";

        timer.SetActive(true);

        while(time > 0f)
        {
            time -= Time.deltaTime;
            timerCounter.text = $"{(int)(time)}";

            yield return null;
        }

        timer.SetActive(false);
        coroutine = null;

        Updating();
        UltaActivator.Instance.Deactivate();
    }
}
