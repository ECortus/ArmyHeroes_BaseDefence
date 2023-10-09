using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks;

public class Level : MonoBehaviour
{
    public void On() => gameObject.SetActive(true);
    public void Off() => gameObject.SetActive(false);
    public void Eliminate() => Destroy(gameObject);

    public FirstResourceOnStart ResourceOnStart;
    public GoldRewardPerWaveChest Chest;
    public EnemiesGenerator Generator;
    public LevelWavesInfo WavesInfo;

    [Space] public DoctorPatientTimers PatientTimers;
    public Transform HealPoint, HealTimersGridParent;

    [Space] public Transform[] SoldiersGoDots;

    [Space] [SerializeField] private UpgraderZone[] upgraderZones;

    public void StartLevel()
    {
        GameManager.Instance.SetActive(true);
        if (LevelManager.Instance.GetIndex() > 0)
        {
            Generator.Launch();
            ResourceOnStart.Spawn();
        }
    }

    public void EndLevel()
    {
        UI.Instance.EndLevel();

        GameManager.Instance.SetActive(false);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;

        Generator.Stop();
        EnemiesPool.Instance.KillAllEnemies();

        ResetLevel();
    }

    public void ResetLevel()
    {
        foreach(var VARIABLE in upgraderZones)
        {
            VARIABLE.Reset();
        }

        PlayerInfo.Instance.SetWeapon(0);
        
        EndLevelStats.Instance.Reset();
        PlayerNewProgress.Instance.ResetAllBonuses();

        Gold.Minus(9999999);
        Crystal.Minus(9999999);
        Experience.Minus(9999999);

        ResourceOnStart.HaveToSpawn = true;
    }
} 
