using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class PlayerNewProgress : MonoBehaviour
{
    public static PlayerNewProgress Instance { get; set; }
    void Awake() => Instance = this;

    public bool Active => menu.activeSelf;

    public List<NewProgressBonus> AllBonuses = new List<NewProgressBonus>();
    [HideInInspector] public List<NewProgressBonus> Bonuses = new List<NewProgressBonus>();

    [SerializeField] private List<NewProgressBonusUI> BonusesUI = new List<NewProgressBonusUI>();
    [SerializeField] private GameObject menu, tip;
    
    private int RequiredCount = 3;

    void Start()
    {
        SetAllBonuses();
    }

    void FormList()
    {
        Bonuses.Clear();

        List<NewProgressBonus> list = new List<NewProgressBonus>();
        list.AddRange(AllBonuses);

        NewProgressBonus bonus = null;

        int index = 0;
        for(int i = 0; i < RequiredCount; i++)
        {
            index = Random.Range(0, list.Count);
            bonus = list[index];

            if(bonus.IsMaxUsed || !bonus.AdditionalContidion || Bonuses.Contains(bonus))
            {
                i--;
                continue;
            }

            Bonuses.Add(bonus);
            list.Remove(bonus);
        }

        RefrestButtons();
    }

    public void On()
    {
        menu.SetActive(true);
        Time.timeScale = 0f;

        FormList();
    }

    public async void Off()
    {
        menu.SetActive(false);
        tip.SetActive(true);

        await UniTask.WaitUntil(() => 
            Input.touchCount > 0 || Input.GetMouseButtonDown(0)
        );

        tip.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RefreshBonus(NewProgressBonus bns)
    {
        bns.SetBonus();
    }

    public void SetAllBonuses()
    {
        foreach(NewProgressBonus bonus in AllBonuses)
        {
            bonus.ApplyCount = 0;
            bonus.SetBonus();
        }
    }

    public void ResetAllBonuses()
    {
        foreach(NewProgressBonus bonus in AllBonuses)
        {
            bonus.Reset();
        }
    }

    public void RefrestButtons()
    {
        foreach(NewProgressBonusUI npb in BonusesUI)
        {
            npb.Refresh();
        }
    }
}
