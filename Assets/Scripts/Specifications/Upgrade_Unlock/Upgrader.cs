using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Upgrader : MonoBehaviour
{
    private string Tags = "Player";

    protected virtual string PreName { get; }
    protected virtual int MaxProgress { get; }
    protected virtual int DefaultCost { get; }
    protected virtual int UpCostPerProgress { get; }

    protected virtual bool ConditionToAllowInter { get; }
    protected virtual void RecourceUse(int amount) { }
    protected virtual void Complete() 
    {
        Progress++;
    }

    public string SaveName => PreName + "_Upgrade";
    public string AmountName => PreName + "_RequireAmount";
    public int Cost => DefaultCost + UpCostPerProgress * Progress;

    [Header("Info zone:")]
    [SerializeField] private TextMeshProUGUI amountText;
    Coroutine coroutine;

    private int RequireAmount
    {
        get
        {
            return PlayerPrefs.GetInt(AmountName, 0);
        }
        set
        {
            PlayerPrefs.SetInt(AmountName, value);
            PlayerPrefs.Save();
        }
    }

    public int Progress
    {
        get
        {
            return PlayerPrefs.GetInt(SaveName, 0);
        }
        set
        {
            PlayerPrefs.SetInt(SaveName, value);
            PlayerPrefs.Save();
        }
    }

    void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        if(Progress != MaxProgress)
        {
            if(RequireAmount == 0) RequireAmount = Cost;
            RefreshText();
        }
        else
        {
            amountText.text = $"--/--";
            this.enabled = false;

            gameObject.SetActive(false);
        }
    }

    void RefreshText()
    {
        amountText.text = $"{Cost - RequireAmount}/{Cost}";
    }

    void StartReduce()
    {
        if(coroutine == null) coroutine = StartCoroutine(ReduceRequiredAmount());
    }

    void StopReduce()
    {
        if(coroutine != null) StopCoroutine(coroutine);
        coroutine = null;
    }

    IEnumerator ReduceRequiredAmount()
    {
        int amount = 1;
        int iter = 0;
        yield return null;

        while(true)
        {
            if(!ConditionToAllowInter) break;
            
            amount = 1;

            RecourceUse(amount);
            RequireAmount -= amount;

            RefreshText();

            if(RequireAmount <= 0)
            {
                Complete();
                Refresh();
                break;
            }

            yield return new WaitForSeconds(0.075f / (1f + iter / 4f));
            iter++;
        }        
    }

    void OnTriggerEnter(Collider col)
    {
        if(!ConditionToAllowInter || !this.enabled) return;

        if(col.tag == Tags)
        {
            StartReduce();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(!this.enabled) return;

        if(col.tag == Tags)
        {
            StopReduce();
        }
    }
}
