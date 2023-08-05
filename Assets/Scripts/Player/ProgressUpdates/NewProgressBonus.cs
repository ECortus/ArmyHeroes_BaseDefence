using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewProgressBonus : ScriptableObject
{
    public string Name => this.name;

    [SerializeField] private int MaxUsedCount = 1;
    public bool IsMaxUsed => UsedCount >= MaxUsedCount;

    public virtual bool AdditionalContidion => true;

    public virtual void Apply()
    {
        
    }

    public virtual void Cancel()
    {

    }

    public int DefaultUsedCount
    {
        get
        {
            return PlayerPrefs.GetInt($"BONUS_{Name}_DEFAULTUSEDCOUNT", 0);
        }
        set
        {
            PlayerPrefs.SetInt($"BONUS_{Name}_DEFAULTUSEDCOUNT", value);
            PlayerPrefs.Save();

            SetBonus();
        }
    }
    
    public int UsedCount
    {
        get
        {
            return PlayerPrefs.GetInt($"BONUS_{Name}_USEDCOUNT", DefaultUsedCount);
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

    [HideInInspector] public int ApplyCount = 0;

    public void SetBonus()
    {
        int count = UsedCount;
        
        if(count <= ApplyCount)
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
