using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalInfo : Detection
{
    void Start()
    {
        Heal(999f);
        Pool();
    }

    void OnDisable()
    {
        Depool();
    }
}
