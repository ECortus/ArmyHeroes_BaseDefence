using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GeneratorUI : MonoBehaviour
{
    public static GeneratorUI Instance { get; set; }
    void Awake() => Instance = this;

    private EnemiesGenerator gen => LevelManager.Instance.ActualLevel.Generator;
    [SerializeField] private TextMeshProUGUI counter;

    public void Refresh()
    {
        counter.text = $"{Mathf.Clamp(gen.WaveIndex + 1, 0, gen.WavesCount)}/{gen.WavesCount}";
    }
}
