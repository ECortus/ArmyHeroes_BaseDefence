using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenUI : FloatingCounter
{
    public static TokenUI Instance { get; set; }
    void Awake() => Instance = this;

    protected override int recource { get => Statistics.Token; }
}
