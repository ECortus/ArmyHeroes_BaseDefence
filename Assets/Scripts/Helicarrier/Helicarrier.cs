using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicarrier : Target
{
    public static Helicarrier Instance { get; set; }
    void Awake() => Instance = this;

    [SerializeField] private GameObject exit;

    public void OnExit()
    {
        exit.SetActive(true);
    }
}
