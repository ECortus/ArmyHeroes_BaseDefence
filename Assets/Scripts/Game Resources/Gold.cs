using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Gold
{
    private static int gold { get { return Statistics.Gold; } set { Statistics.Gold = value; } }

    public static void Plus(int count) 
    {
        gold += count;

        GoldUI.Instance.Refresh();
    }
    
    public static void Minus(int count)
    { 
        gold -= count;
        if(gold < 0) gold = 0;

        GoldUI.Instance.Refresh();
    }
}
