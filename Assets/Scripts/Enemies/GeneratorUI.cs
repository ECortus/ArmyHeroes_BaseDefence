using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GeneratorUI : MonoBehaviour
{
    [SerializeField] private EnemiesGenerator gen;
    [SerializeField] private TextMeshProUGUI counter;

    public void Refresh()
    {
        counter.text = $"{Mathf.Clamp(gen.WaveIndex + 1, 0, gen.WavesCount)}/{gen.WavesCount}";
    }
}
