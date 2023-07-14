using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class EnemiesGenerator : MonoBehaviour
{
    [SerializeField] private LevelWavesInfo info;
    [SerializeField] private Transform MainTargetForEnemies;
    [SerializeField] private float Radius = 30f;
    private Vector3 Center
    {
        get
        {
            return transform.position + new Vector3(0f, 1f, 0f);
        }
    }

    private int _wi = 0;
    public int WaveIndex
    {
        get
        {
            return _wi;
        }
        set
        {
            _wi = value;
            counter.Refresh();
        }
    }
    public int WavesCount => info.Waves.Count;

    [SerializeField] private GeneratorUI counter;

    Coroutine coroutine;

    void Start()
    {
        Launch();
    }

    public void Launch()
    {
        if(coroutine == null) coroutine = StartCoroutine(Working());
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
        yield return new WaitForSeconds(1f);

        Enemy enemy = null;
        Vector3 position = Vector3.zero;
        WaveIndex = 0;

        foreach(Wave wave in info.Waves)
        {
            foreach(Slot slot in wave.Slots)
            {
                foreach(Call call in slot.Calls)
                {
                    for(int i = 0; i < call.Count; i++)
                    {
                        enemy = Spawn(call.enemy, GetPosition());
                        
                        enemy.SetMainTarget(MainTargetForEnemies);
                        enemy.SetAdditionalTarget(MainTargetForEnemies);
                    }

                    yield return new WaitForSeconds(info.DelayBetweenCalls);
                }

                yield return new WaitForSeconds(Mathf.Clamp(info.DelayBetweenSlots - info.DelayBetweenCalls, 0f, 999f));
            }

            yield return new WaitUntil(() => EnemiesPool.Instance.AllDied);
            WaveIndex++;
        }

        Stop();
    }

    Enemy Spawn(Enemy nm, Vector3 pos)
    {
        Quaternion rot = Quaternion.Euler(0f, Vector3.Angle((pos - Center).normalized, transform.forward), 0f);
        return EnemiesPool.Instance.Insert(nm.Type, nm, pos, rot);
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
