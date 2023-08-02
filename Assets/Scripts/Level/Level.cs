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

    public EnemiesGenerator Generator;
    public LevelWavesInfo WavesInfo;

    public void StartLevel()
    {
        GameManager.Instance.SetActive(true);
        Generator.Launch();
    }

    public void EndLevel()
    {
        UI.Instance.EndLevel();

        GameManager.Instance.SetActive(false);
    }

    public void ResetLevel()
    {
        Time.timeScale = 1f;

        Generator.Stop();
        EnemiesPool.Instance.KillAllEnemies();

        PlayerNewProgress.Instance.ResetAllBonuses();
    }
} 
