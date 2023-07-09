using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FloatingCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recourceText;
    [SerializeField] private float counterPlusBySecond = 100f;
    [SerializeField] private float bound = 3;
    [SerializeField] private bool forced = false;

    protected virtual int recource { get; }
    int currentMoneyCount = 0;

    Coroutine coroutine;
    int sign
    {
        get
        {
            if(currentMoneyCount > recource) return -1;
            else if (currentMoneyCount < recource) return 1;
            else return 0;
        }
    }
    void Start()
    {
        Reset();
    }

    public void Refresh()
    {
        if(forced)
        {
            Reset();
            return;
        }

        if(currentMoneyCount != recource) 
        {
            Reset(); 
            return;
        }

        if(coroutine == null) coroutine = StartCoroutine(Coroutine());
    }

    public void Reset()
    {
        currentMoneyCount = recource;
        IntoText(currentMoneyCount);
    }

    IEnumerator Coroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.014f);

        while(currentMoneyCount != recource)
        {
            currentMoneyCount = (int)Mathf.Lerp(currentMoneyCount, recource, counterPlusBySecond * Time.deltaTime);
            if (Mathf.Abs(currentMoneyCount - recource) <= bound) break;

            IntoText(currentMoneyCount);
            yield return wait;
        }

        currentMoneyCount = recource;
        IntoText(currentMoneyCount);
        yield return null;

        StopCoroutine(coroutine);
        coroutine = null;
    }

    void IntoText(int value)
    {
        recourceText.text = RecourceAmountConvertator.IntIntoText(value);
    }
}
