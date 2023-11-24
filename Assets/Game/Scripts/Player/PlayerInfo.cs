using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;

public class PlayerInfo : Detection
{
    public static PlayerInfo Instance { get; set; }
    [Inject] void Awake() => Instance = this;

    [Header("Player info: ")]
    [SerializeField] private GunHandler gunHandler;

    public int WeaponIndex
    {
        get
        {
            return PlayerPrefs.GetInt(DataManager.WeaponKey, 0);
        }
        set
        {
            PlayerPrefs.SetInt(DataManager.WeaponKey, value);
            PlayerPrefs.Save();
        }
    }

    public void SetWeapon(int i)
    {
        WeaponIndex = i;
        gunHandler.SetGunPair(i);
    }

    [SerializeField] private float DefaultMaxExp = 100f, ExpRequirePerProgress = 50f;

    public float MaxExperience => DefaultMaxExp + Progress * ExpRequirePerProgress;

    void Start()
    {
        Heal(999f);
        SetWeapon(WeaponIndex);
    }

    public int Progress
    {
        get
        {
            return PlayerPrefs.GetInt(DataManager.ProgressKey, 0);
        }
        set
        {
            PlayerPrefs.SetInt(DataManager.ProgressKey, value);
            PlayerPrefs.Save();
        }
    }

    public void GetNewProgress()
    {
        Progress++;
        PlayerNewProgress.Instance.On();
    }

    public void ResetProgress()
    {
        Progress = 0;
    }

    public override void Death()
    {
        base.Death();
        Time.timeScale = 0f;
    }

    float addedPercent = 0f;
    float percentPerSecond
    {
        get
        {
            return addedPercent + PlayerUpgradesLVLs.RegenMod;
        }
    }
    Coroutine coroutine;

    public float GetRegenPercent() => percentPerSecond;

    public void AddRegenPercent(float pps)
    {
        addedPercent += pps;
        if(percentPerSecond > 0f)
        {
            StartAutoRegeneration();
        }
    }

    void StartAutoRegeneration()
    {
        if(coroutine == null)
        {
            coroutine = StartCoroutine(AutoRegeneration());
        }
    }   

    public void StopAutoRegeneration()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        addedPercent = 0f;
    }

    public void ResetAutoRegeneration()
    {
        addedPercent = 0f;
        if(percentPerSecond <= 0f)
        {
            StopAutoRegeneration();
        }
    }

    IEnumerator AutoRegeneration()
    {
        while(true)
        {
            yield return new WaitForSeconds(60f);
            Heal(MaxHP * percentPerSecond);
        }
    }
}
