using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using TMPro;

public class OnStartUI : MonoBehaviour
{
    public static OnStartUI Instance { get; set; }
    void Awake() => Instance = this;

    [SerializeField] private GameObject start, main;

    [Space]
    [SerializeField] private TextMeshProUGUI chapterText;
    [SerializeField] private TextMeshProUGUI gold, crystal;

    public async void On()
    {
        main.SetActive(false);
        start.SetActive(true);

        chapterText.text = $"Chapter {LevelManager.Instance.GetIndex()}";
        gold.text = $"{Statistics.Gold}";
        crystal.text = $"{Statistics.Crystal}";

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
