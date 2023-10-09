using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using TMPro;

public class OnStartUI : MonoBehaviour
{
    public static OnStartUI Instance { get; set; }
    void Awake() => Instance = this;

    private EnemiesGenerator gen => LevelManager.Instance.ActualLevel.Generator;

    [SerializeField] private GameObject start, main, info;

    [Space]
    [SerializeField] private TextMeshProUGUI chapterText;
    [SerializeField] private TextMeshProUGUI wave, gold, crystal;

    public async void On()
    {
        main.SetActive(false);
        start.SetActive(true);

        if (Statistics.LevelIndex > 0)
        {
            info.SetActive(true);
            
            chapterText.text = $"Chapter {LevelManager.Instance.GetIndex()}";
            gold.text = $"{Statistics.Gold}";
            wave.text = $"{Mathf.Clamp(gen.WaveIndex + 1, 0, gen.WavesCount)}/{gen.WavesCount}";
            crystal.text = $"{Statistics.Crystal}";
        }
        else
        {
            info.SetActive(false);
        }

        Time.timeScale = 0f;

        await UniTask.WaitUntil(() => Input.touchCount > 0 || Input.GetMouseButton(0));

        Off();
    }

    public void Off()
    {
        main.SetActive(true);
        start.SetActive(false);

        Time.timeScale = 1f;
        LevelManager.Instance.ActualLevel.StartLevel();
    }
}
