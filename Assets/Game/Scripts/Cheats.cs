using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    public void PlusGold()
    {
        Gold.Plus(5000);
    }

    public void PlusCrystal()
    {
        Crystal.Plus(5000);
    }

    public void PlusToken()
    {
        Token.Plus(50);
    }

    public void NewPlayerProgress()
    {
        Experience.Plus(99999);
    }
}
