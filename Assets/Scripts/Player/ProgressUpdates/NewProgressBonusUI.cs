using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewProgressBonusUI : MonoBehaviour
{
    public int index => System.Int32.Parse(gameObject.name);
    public bool ContainInBonuses => 
        PlayerNewProgress.Instance.Bonuses.Contains(PlayerNewProgress.Instance.AllBonuses[index]);

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        PlayerNewProgress.Instance.AllBonuses[index].Add();
        PlayerNewProgress.Instance.Off();
    }

    public void Refresh()
    {
        if(ContainInBonuses)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
