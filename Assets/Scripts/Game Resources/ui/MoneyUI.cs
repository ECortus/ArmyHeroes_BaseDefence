using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using TMPro;

public class MoneyUI : FloatingCounter
{
    public static MoneyUI Instance { get; set; }
    void Awake() => Instance = this;

    protected override int recource { get => Statistics.Money; }
}
