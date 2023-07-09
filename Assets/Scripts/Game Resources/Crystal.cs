using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Crystal
{
    private static int crystal { get { return Statistics.Crystal; } set { Statistics.Crystal = value; } }

    public static void Plus(int count) 
    {
        crystal += count;

        CrystalUI.Instance.Refresh();
    }
    
    public static void Minus(int count)
    { 
        crystal -= count;
        if(crystal < 0) crystal = 0;

        CrystalUI.Instance.Refresh();
    }
}
