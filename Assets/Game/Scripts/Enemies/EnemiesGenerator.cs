using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class EnemiesGenerator : MonoBehaviour
{
    private LevelWavesInfo info => LevelManager.Instance.ActualLevel.WavesInfo;
    
    [SerializeField] private Transform MainTargetForEnemies;
    [SerializeField] private float Radius = 30f;

    private Vector3 Center
    {
        get
        {
            return transform.position + new Vector3(0f, 1f, 0f);
        }
    }

    public int WaveIndex
    {
        get
        {
            return EndLevelStats.Instance.WaveIndex;
        }
        set
        {
            EndLevelStats.Instance.WaveIndex = value;
            GeneratorUI.Instance.Refresh();
        }
    }

    public int WavesCount => info.Count;

    public bool Active => coroutine != null;
    Coroutine coroutine;

    public void Launch()
    {
        coroutine ??= StartCoroutine(Working());
    }

    public void Stop()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    IEnumerator Working()
    {
        /*GeneratorUI.Instance.ResetSlider();*/
        
        Wave wave;
        Enemy enemy = null;
        Vector3 position = Vector3.zero;

        for(int w = Mathf.Clamp(WaveIndex, 0, WavesCount - 1); w < WavesCount; w++)
        {
            WaveIndex = w;
            wave = info.Waves[w];

            yield return PullOutWave(wave);
            yield return new WaitUntil(() => EnemiesPool.Instance.AllDied);
            
            WaveIndex++;
            GeneratorUI.Instance.ResetSlider();
            
            PlayAllHappy();
            
            if (w + 1 < WavesCount)
            {
                LevelManager.Instance.ActualLevel.Chest.On();
            }
        }

        /* UI.Instance.EndLevel(); */
        Helicarrier.Instance.OnExit();

        bool infinity = true;
        if(infinity)
        {
            while(true)
            {
                enemy = Spawn(GetRandomEnemy(), GetPosition());
                yield return new WaitForSeconds(1f);
            }
        }

        Stop();
    }

    public IEnumerator PullOutWave(Wave wave)
    {
        GeneratorUI.Instance.ResetSlider();
        
        Enemy enemy = null;
        Vector3 position = Vector3.zero;
        
        foreach(Slot slot in wave.Slots)
        {
            foreach(Call call in slot.Calls)
            {
                for(int i = 0; i < call.Count; i++)
                {
                    enemy = Spawn(call.enemy, GetPosition());
                }
                yield return new WaitForSeconds(info.DelayBetweenCalls);
            }

            yield return new WaitForSeconds(Mathf.Clamp(info.DelayBetweenSlots - info.DelayBetweenCalls, 0f, 999f));
        }
    }

    public void PlayAllHappy()
    {
        DetectType types = DetectType.Engineer | DetectType.Pikeman | DetectType.Soldier | DetectType.Doctor;
        Detection[] allies = DetectionPool.Instance.RequirePools(types);

        foreach(var VARIABLE in allies)
        {
            if(!VARIABLE.Died && VARIABLE.Active) VARIABLE.EmojiesController.PlayHappy();
        }
    }

    Enemy GetRandomEnemy()
    {
        int wave = Random.Range(0, WavesCount);
        int slot = Random.Range(0, info.Waves[wave].Slots.Length);
        int call = Random.Range(0, info.Waves[wave].Slots[slot].Calls.Length);
        Enemy enemy = info.Waves[wave].Slots[slot].
            Calls[Random.Range(0, info.Waves[wave].Slots[slot].Calls.Length)].enemy;

        return enemy;
    }

    public Enemy Spawn(Enemy nm, Vector3 pos)
    {
        Quaternion rot = Quaternion.Euler(0f, Vector3.Angle((pos - Center).normalized, transform.forward), 0f);
        Enemy enemy = EnemiesPool.Instance.Insert(nm.Type, nm, pos, rot);
        
        enemy.SetMainTarget(MainTargetForEnemies);
        enemy.SetAdditionalTarget(MainTargetForEnemies);
        
        return enemy;
    }

    Vector3 GetPosition()
    {
        Vector3 point = RandomPointOnCircleEdge(Radius);
        Vector3 pos = Center + point;

        if(Vector3.Distance(Player.Instance.transform.position, pos) < Radius * 0.5f)
        {
            return GetPosition();
        }
        else
        {
            return pos;
        }
    }

    private Vector3 RandomPointOnCircleEdge(float radius)
    {
        var vector2 = Random.insideUnitCircle.normalized * radius;
        return new Vector3(vector2.x, 0, vector2.y);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(Center, Radius);
    }
}
