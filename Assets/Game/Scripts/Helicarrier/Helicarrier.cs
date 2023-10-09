using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Helicarrier : Target
{
    public static Helicarrier Instance { get; set; }
    void Awake() => Instance = this;

    [SerializeField] private PlayerExitFromHeli exitFromHeli;
    [SerializeField] private Animation goAnim;
    [SerializeField] private Transform exitCamera;
    [SerializeField] private GameObject exit;

    public void OnExit()
    {
        exit.SetActive(true);
    }

    public async void Exit()
    {
        Player.Instance.gameObject.SetActive(false);
        CameraController.Instance.SetTarget(exitCamera);
        await exitFromHeli.Enter();

        goAnim.Play("HeliGo");
        await UniTask.Delay((int)(goAnim.GetClip("HeliGo").length * 1000));
        
        LevelManager.Instance.EndLevel();
    }
}
