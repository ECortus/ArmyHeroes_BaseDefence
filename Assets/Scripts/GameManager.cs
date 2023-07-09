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

    [SerializeField] private float TimeScale = 3f;
    public FloatingJoystick Joystick;

    void Awake() => Instance = this;

    void Start()
    {
        Application.targetFrameRate = 60;
        Time.timeScale = TimeScale;
    }

    public void SetFollowTarget(Transform tf)
    {
        
    }

    /* void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Money.Plus(53453452345653436745f);
        }
    } */
}
