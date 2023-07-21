using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewProgressBonus : ScriptableObject
{
    public string Name => this.name;

    [SerializeField] private int MaxUsedCount = 1;
    public bool IsMaxUsed => UsedCount >= MaxUsedCount;

    public virtual void Apply()
    {
        
    }

    public virtual void Cancel()
    {

    }
    
    public int UsedCount
    {
        get
        {
            return PlayerPrefs.GetInt($"BONUS_{Name}_USEDCOUNT", 0);
        }
        set
        {
            PlayerPrefs.SetInt($"BONUS_{Name}_USEDCOUNT", value);
            PlayerPrefs.Save();

            SetBonus();
        }
    }

    public void Add()
    {
        if(!IsMaxUsed)
        {
            UsedCount++;
        }
    }

    public void Reset()
    {
        if(UsedCount > 0)
        {
            UsedCount = 0;
        }
    }

    public int ApplyCount { get; set; }

    public void SetBonus()
    {
        int count = UsedCount;
        
        if(count < ApplyCount)
        {
            ApplyCount = 0; 
            Cancel();
        }
        else
        {
            count -= ApplyCount;
        }

        for(int i = 0; i < count; i++)
        {
            ApplyCount++;
            Apply();
        }
    }
}
