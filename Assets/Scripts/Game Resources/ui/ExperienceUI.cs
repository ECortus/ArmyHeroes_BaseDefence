using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceUI : MonoBehaviour
{
    public static ExperienceUI Instance { get; set; }
    void Awake() => Instance = this;

    private float recource { get => Statistics.Experience; }
    [SerializeField] private Slider slider;
    private float maxValue /* => PlayerInfo.Instance.MaxExperience */;

    void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        slider.minValue = 0f;
        slider.maxValue = maxValue;

        slider.value = recource;
    }
}
