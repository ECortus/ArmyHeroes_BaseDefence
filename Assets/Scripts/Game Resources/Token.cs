using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Token
{
    private static int token { get { return Statistics.Token; } set { Statistics.Token = value; } }

    public static void Plus(int count) 
    {
        token += count;

        TokenUI.Instance.Refresh();
    }
    
    public static void Minus(int count)
    { 
        token -= count;
        if(token < 0) token = 0;

        TokenUI.Instance.Refresh();
    }
}
