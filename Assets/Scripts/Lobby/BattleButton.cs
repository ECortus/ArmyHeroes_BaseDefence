using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waveCounter;

    [Space]
    [SerializeField] private LevelWavesInfo[] infos;

    private int levelIndex { get { return Statistics.LevelIndex; } set { Statistics.LevelIndex = value; } }
    public int GetIndex() => levelIndex % infos.Length;

    int waveIndex => PlayerPrefs.GetInt(DataManager.WaveIndexKey, 0);
    int waveCount => infos[GetIndex()].Waves.Length;

    void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        waveCounter.text = $"{waveIndex}/{waveCount}";
    }
}
