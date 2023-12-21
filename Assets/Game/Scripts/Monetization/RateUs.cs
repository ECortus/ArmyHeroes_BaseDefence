using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateUs : MonoBehaviour
{
    [SerializeField] private RateUsUI ui;
    [SerializeField] private int tryingToOpen = 3;

    private float time;

    private void Update()
    {
        time -= Time.deltaTime;
    }

    private int trying
    {
        get
        {
            return PlayerPrefs.GetInt("RateUsKey", 0);
        }
        set
        {
            PlayerPrefs.SetInt("RateUsKey", value);
            PlayerPrefs.Save();
        }
    }

    private void OnDestroy()
    {
        trying = 0;
    }

    public void TryOpen()
    {
        if (time < 0f)
        {
            trying++;
            time = 30f;
            
            if (trying == tryingToOpen)
            {
                ui.Open();
            }
        }
    }
}
