using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseLevelUI : OpenCloseObjectLevelUI
{
    [SerializeField] private EndLevelStatsUI statsUI;

    public override void Open()
    {
        base.Open();
        statsUI.Refresh();
    }
}
