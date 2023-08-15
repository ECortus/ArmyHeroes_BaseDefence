using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndLevelStatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI wavesText, timeText, killingText, tokenText;

    void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        wavesText.text = $"{EndLevelStats.Instance.WaveIndex}/{EndLevelStats.Instance.WaveCount}";
        timeText.text = $"{TimeToString()}";
        killingText.text = $"{EndLevelStats.Instance.KillingCount}";
        tokenText.text = $"{EndLevelStats.Instance.TokenReward}";
    }

    string TimeToString()
    {
        string text = "";
        int time = (int)EndLevelStats.Instance.Time;

        string seconds = $"{(time % 60).ToString()}";
        if(seconds.Length < 2) seconds = "0" + seconds;

        string minutes = $"{(time / 60).ToString()}";
        if(minutes.Length < 2) minutes = "0" + minutes;

        if(time / 3600 > 0)
        {
            string hours = $"{(time / 3600).ToString()}";
            if(hours.Length < 2) hours = "0" + hours;

            text += $"{hours}:";;
        }

        text += $"{minutes}:{seconds}";
        return text;
    }
}
