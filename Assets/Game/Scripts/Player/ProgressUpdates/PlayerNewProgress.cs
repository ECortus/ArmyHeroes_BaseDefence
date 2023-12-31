using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;

public class PlayerNewProgress : MonoBehaviour
{
    public static PlayerNewProgress Instance { get; set; }
    [Inject] void Awake() => Instance = this;

    public bool Active => menu.activeSelf;

    public List<NewProgressBonus> AllBonuses = new List<NewProgressBonus>();
    [HideInInspector] public List<NewProgressBonus> Bonuses = new List<NewProgressBonus>();

    [SerializeField] private List<NewProgressBonusUI> BonusesUI = new List<NewProgressBonusUI>();
    [SerializeField] private GameObject menu, tip;

    [Space]
    [SerializeField] private GameObject buttonTripleX;
    [SerializeField] private GameObject buttonTripleXNoAd;
    
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

    public void OnWith3X()
    {
        buttonTripleX.SetActive(Statistics.LevelIndex > 0);
        buttonTripleXNoAd.SetActive(Statistics.LevelIndex <= 0);
        
        On();
    }
    
    public void GetAll3XAbilitiesNoAd()
    {
        foreach(NewProgressBonus npb in Bonuses)
        {
            npb.Add();
        }
        Off();
    }

    public async void GetAll3XAbilities()
    {
        if (await GameAdsController.Instance.ShowRewardAd())
        {
            foreach(NewProgressBonus npb in Bonuses)
            {
                npb.Add();
            }
        }
        
        Off();
    }

    public void On()
    {
        menu.SetActive(true);
        Time.timeScale = 0f;

        FormList();
    }

    public async void Off()
    {
        buttonTripleX.SetActive(false);
        buttonTripleXNoAd.SetActive(false);
        
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

        PlayerUpgrades.Instance.SetPermanentBonuses();
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
