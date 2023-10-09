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

    protected virtual int resource { get; }
    int currentMoneyCount = 0;

    Coroutine coroutine;
    int sign
    {
        get
        {
            if(currentMoneyCount > resource) return -1;
            else if (currentMoneyCount < resource) return 1;
            else return 0;
        }
    }
    void Start()
    {
        Reset();
    }

    public void Refresh()
    {
        if(forced || !gameObject.activeInHierarchy || currentMoneyCount == resource)
        {
            Reset();
            return;
        }

        coroutine ??= StartCoroutine(Coroutine());
    }

    public void Reset()
    {
        currentMoneyCount = resource;
        IntoText(currentMoneyCount);
    }

    IEnumerator Coroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.014f);

        while(currentMoneyCount != resource)
        {
            currentMoneyCount = (int)Mathf.Lerp(currentMoneyCount, resource, counterPlusBySecond * Time.deltaTime);
            if (Mathf.Abs(currentMoneyCount - resource) <= bound) break;

            IntoText(currentMoneyCount);
            yield return wait;
        }

        currentMoneyCount = resource;
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
