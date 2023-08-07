using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewProgressBonusUI : MonoBehaviour
{
    [SerializeField] private NewProgressBonus bonus;
    public bool ContainInBonuses => 
        PlayerNewProgress.Instance.Bonuses.Contains(bonus);

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    public void AddBonus()
    {
        bonus.Add();
    }

    public void OnButtonClick()
    {
        AddBonus();
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
