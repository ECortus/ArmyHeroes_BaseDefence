using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Tutorial : MonoBehaviour
{
    public static Tutorial Instance { get; set; }
    void Awake() => Instance = this;

    [SerializeField] private TutorialArrow Arrow;
    private LevelWavesInfo info => LevelManager.Instance.ActualLevel.WavesInfo;
    private EnemiesGenerator Generator => LevelManager.Instance.ActualLevel.Generator;

    [Header("TUTOR TARGETS: ")]
    [SerializeField] private Transform heli;
    [SerializeField] private Transform soldierUnlock, chest, exit;
    [SerializeField] private GameObject ultaUI;
    
    [Space]
    [SerializeField] private NewAbilitiesChestSpawner newAbilitiesChestSpawner;
    
    private Coroutine coroutine;

    private void Start()
    {
        StartTutorial();
    }

    void StartTutorial()
    {
        coroutine ??= StartCoroutine(Process());
    }

    IEnumerator Process()
    {
        ultaUI.SetActive(false);
        
        LevelManager.Instance.ActualLevel.ResetLevel();
        
        Arrow.Off();
        yield return new WaitUntil(() => Generator.Active);
        Generator.Stop();
        EnemiesPool.Instance.KillAllEnemies();

        yield return Generator.PullOutWave(info.Waves[0]);
        Arrow.On();
        Arrow.SetTarget(EnemiesPool.Instance.EnemyTutorPool[0].Transform);
        yield return new WaitUntil(() => EnemiesPool.Instance.AllDied);
        
        LevelManager.Instance.ActualLevel.Chest.On();
        Arrow.SetTarget(chest);
        yield return new WaitUntil(() => !LevelManager.Instance.ActualLevel.Chest.gameObject.activeSelf);
        
        soldierUnlock.gameObject.SetActive(true);
        Arrow.SetTarget(soldierUnlock);
        DetectType type = DetectType.Soldier;
        yield return new WaitUntil(() => DetectionPool.Instance.RequirePools(type).Length > 0);
        
        Transform abChest = newAbilitiesChestSpawner.SpawnOneByIndex(0);
        Arrow.SetTarget(abChest.transform);
        
        yield return new WaitUntil(() => !abChest.gameObject.activeSelf);
        Arrow.Off();
        
        yield return Generator.PullOutWave(info.Waves[1]);
        yield return new WaitUntil(() => EnemiesPool.Instance.AllDied);
        
        Helicarrier.Instance.OnExit();
        Arrow.On();
        Arrow.SetTarget(exit);

        yield return new WaitUntil(() => !Player.Instance.Active);
        
        Arrow.Off();
        LevelManager.Instance.ActualLevel.ResetLevel();
    }
}
