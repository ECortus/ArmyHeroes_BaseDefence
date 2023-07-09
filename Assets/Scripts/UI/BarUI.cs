using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarUI : MonoBehaviour
{
    protected virtual float Amount { get; set; }
    protected virtual float MaxAmount { get; set; }

    [SerializeField] private Slider slider;

    void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        slider.minValue = 0f;
        slider.maxValue = MaxAmount;

        slider.value = Amount;
    } 
}
