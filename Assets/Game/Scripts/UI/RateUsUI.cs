using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateUsUI : OpenCloseObjectLevelUI
{
    public override void Open()
    {
        Time.timeScale = 0f;
        base.Open();
    }

    public override void Close()
    {
        Time.timeScale = 1f;
        base.Close();
    }
}
