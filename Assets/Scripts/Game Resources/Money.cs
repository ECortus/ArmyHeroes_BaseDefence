using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Money
{
    private static int money { get { return Statistics.Money; } set { Statistics.Money = value; } }

    public static void Plus(int count) 
    {
        money += count;

        MoneyUI.Instance.Refresh();
    }
    
    public static void Minus(int count)
    { 
        money -= count;
        if(money < 0) money = 0;

        MoneyUI.Instance.Refresh();
    }
}
