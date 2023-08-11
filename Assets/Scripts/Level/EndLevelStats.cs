using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EndLevelStats : MonoBehaviour
{
    [Inject] public static EndLevelStats Instance { get; set; }
    [Inject] void Awake() => Instance = this;

    public Level Level => LevelManager.Instance.ActualLevel;

    public int WaveIndex
    {
        get
        {
            return PlayerPrefs.GetInt(DataManager.WaveIndexKey, 0);
        }
        set
        {
            PlayerPrefs.SetFloat(DataManager.WaveIndexKey, value);
            PlayerPrefs.Save();
        }
    }

    public int WaveCount => Level.WavesInfo.Count;

    public float Time
    {
        get
        {
            return PlayerPrefs.GetFloat(DataManager.PlayTimeKey, 0f);
        }
        set
        {
            PlayerPrefs.SetFloat(DataManager.PlayTimeKey, value);
            PlayerPrefs.Save();
        }
    }

    public int KillingCount
    {
        get
        {
            return PlayerPrefs.GetInt(DataManager.KillingCountKey, 0);
        }
        set
        {
            PlayerPrefs.SetInt(DataManager.KillingCountKey, value);
            PlayerPrefs.Save();
        }
    }

    public void PlusKillingCount() => KillingCount++;

    public int TokenReward
    {
        get
        {
            return Level.WavesInfo.TokenReward * WaveIndex / WaveCount;
        }
    }

    public void GiveReward()
    {
        Token.Plus(TokenReward);
    }

    public void Reset()
    {
        WaveIndex = 0;
        Time = 0f;
        KillingCount = 0;
    }
}
