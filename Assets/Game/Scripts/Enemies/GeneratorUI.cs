using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GeneratorUI : MonoBehaviour
{
    public static GeneratorUI Instance { get; set; }
    void Awake() => Instance = this;

    private EnemiesGenerator gen => LevelManager.Instance.ActualLevel.Generator;
    
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI counter;

    void OnEnable()
    {
        Refresh();
    }

    public void ResetSlider()
    {
        int max = 0;

        Slot[] wave = LevelManager.Instance.ActualLevel.WavesInfo.Waves[gen.WaveIndex].Slots;

        for (int i = 0; i < wave.Length; i++)
        {
            for (int j = 0; j < wave[i].Calls.Length; j++)
            {
                max += wave[i].Calls[j].Count;
            }
        }
        
        slider.minValue = 0;
        slider.maxValue = max;
        slider.value = 0;
    }

    public void UpdateSlider(int amountPlus)
    {
        slider.value += amountPlus;
    }

    public void Refresh()
    {
        counter.text = $"{Mathf.Clamp(gen.WaveIndex + 1, 0, gen.WavesCount)}/{gen.WavesCount}";
    }
}
