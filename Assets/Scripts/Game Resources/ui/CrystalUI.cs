using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalUI : FloatingCounter
{
    public static CrystalUI Instance { get; set; }
    void Awake() => Instance = this;

    protected override int resource { get => Statistics.Crystal; }
}
