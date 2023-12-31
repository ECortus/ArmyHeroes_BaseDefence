using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgraderZone : MonoBehaviour
{
    private string Tags = "Player";

    private string PreName => name;
    
    [Header("Info: ")] 
    [SerializeField] private int MinProgress;
    [SerializeField] private int MaxProgress;
    [SerializeField] private int DefaultCost;
    [SerializeField] private int UpCostPerProgress;

    protected virtual bool ConditionToAllowInter { get; }
    protected virtual void RecourceUse(int amount) { }
    protected virtual void Complete() 
    {
        Progress++;
        RequireAmount = Cost;
        
        Refresh();
    }

    public string SaveName => PreName + gameObject.name + "_Upgrade_level_" + Statistics.LevelIndex;
    public string AmountName => PreName + gameObject.name + "_RequireAmount_level_" + Statistics.LevelIndex;
    public int Cost => (int)((DefaultCost + UpCostPerProgress * (Progress - MinProgress)) * BuildingUpgradesLVLs.CostMod);

    [Header("Info zone:")]
    [SerializeField] private Slider amountSlider;
    [SerializeField] private TextMeshProUGUI amountText;
    Coroutine coroutine;

    private int RequireAmount
    {
        get
        {
            return Mathf.Clamp(PlayerPrefs.GetInt(AmountName, 0), 0, Cost);
        }
        set
        {
            value = Mathf.Clamp(value, 0, Cost);
            PlayerPrefs.SetInt(AmountName, value);
            PlayerPrefs.Save();
        }
    }

    public int Progress
    {
        get
        {
            return PlayerPrefs.GetInt(SaveName, MinProgress);
        }
        set
        {
            PlayerPrefs.SetInt(SaveName, value);
            PlayerPrefs.Save();
        }
    }

    protected virtual void OnEnable()
    {
        if(RequireAmount == 0) RequireAmount = Cost;
        Refresh();
    }

    public void Reset()
    {
        Progress = MinProgress;
        Refresh();
    }

    public void Refresh()
    {
        if(Progress < MaxProgress)
        {
            RefreshOutInfo();
        }
        else
        {
            StopReduce();

            amountText.text = $"--/--";
            amountSlider.value = amountSlider.maxValue;

            this.enabled = false;

            /* gameObject.SetActive(false); */
        }
    }

    void RefreshOutInfo()
    {
        amountSlider.minValue = 0f;
        amountSlider.maxValue = Cost;
        amountSlider.value = Cost - RequireAmount;

        amountText.text = $"{Cost - RequireAmount}/{Cost}";
    }

    void StartReduce()
    {
        if(coroutine == null && Statistics.Gold > 0)
        {
            coroutine = StartCoroutine(ReduceRequiredAmount());
            CoinThrow.Instance.On(transform);
        }
    }

    void StopReduce()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        CoinThrow.Instance?.Off();
    }

    IEnumerator ReduceRequiredAmount()
    {
        int amount = 1;
        int iter = 0;
        yield return null;

        while(Statistics.Gold > 0)
        {
            if(!ConditionToAllowInter) break;

            RecourceUse(amount);
            RequireAmount -= amount;

            RefreshOutInfo();

            if(RequireAmount <= 0)
            {
                Complete();
                iter = 0;

                /* StopReduce();
                break; */
            }

            yield return new WaitForSeconds(0.05f / (1f + iter / 4f));
            iter++;
        }

        StopReduce();        
    }

    void OnTriggerStay(Collider col)
    {
        if(!this.enabled) return;

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
