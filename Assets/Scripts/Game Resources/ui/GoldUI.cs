using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using TMPro;

public class GoldUI : FloatingCounter
{
    public static GoldUI Instance { get; set; }
    void Awake() => Instance = this;

    protected override int recource { get => Statistics.Gold; }
}
