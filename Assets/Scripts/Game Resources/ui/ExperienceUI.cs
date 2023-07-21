using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExperienceUI : MonoBehaviour
{
    public static ExperienceUI Instance { get; set; }
    void Awake() => Instance = this;

    private float recource { get => Statistics.Experience; }
    private int Progress => PlayerInfo.Instance.Progress;

    [SerializeField] private Slider slider;
    private float maxValue => PlayerInfo.Instance.MaxExperience;

    [SerializeField] private TextMeshProUGUI current, next;

    void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        slider.minValue = 0f;
        slider.maxValue = maxValue;

        slider.value = recource;

        current.text = $"{Progress}";
        next.text = $"{Progress + 1}";
    }
}
