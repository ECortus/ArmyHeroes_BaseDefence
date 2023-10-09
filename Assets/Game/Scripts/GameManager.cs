using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }
    private bool _GameActive = false;
    public void SetActive(bool value) => _GameActive = value;
    public bool isActive => _GameActive;

    public Transform Camera;
    public FloatingJoystick Joystick;

    void Awake() => Instance = this;

    void Start()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    public void SetTimeScale(float time)
    {
        Time.timeScale = time;
    }
}
