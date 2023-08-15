using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPool : MonoBehaviour
{
    public static EnemiesPool Instance;
    void Awake() => Instance = this;

    public List<Enemy> Enemy00Pool = new List<Enemy>();

    public void GetHitAllLowEnemies(Vector3 center, float distance, float hit)
    {
        List<Enemy> enemies = new List<Enemy>();
        enemies.AddRange(Enemy00Pool);

        foreach(Enemy enemy in enemies)
        {
            if((center - enemy.transform.position).magnitude < distance)
            {
                if(enemy.Active && !enemy.Died)
                {
                    enemy.detection.GetHit(hit);
                }
            }
        }
    }

    public List<Enemy> GetAllEnemiesOnDistance(Vector3 center, float distance)
    {
        List<Enemy> enemies = new List<Enemy>();
        enemies.AddRange(Enemy00Pool);

        List<Enemy> list = new List<Enemy>();

        foreach(Enemy enemy in enemies)
        {
            if((center - enemy.transform.position).magnitude < distance)
            {
                list.Add(enemy);
            }
        }

        return list;
    }

    public void KillAllEnemies()
    {
        List<Enemy> enemies = new List<Enemy>();
        enemies.AddRange(Enemy00Pool);

        foreach(Enemy enemy in enemies)
        {
            enemy.Off();
        }
    }

    public Enemy Insert(EnemyType type, Enemy enemy, Vector3 pos = new Vector3(), Quaternion rot = new Quaternion())
    {
        Enemy enm = null;
        List<Enemy> list = new List<Enemy>();

        switch(type)
        {
            case EnemyType.Enemy00:
                list = Enemy00Pool;
                break;
            default:
                break;
        }

        if(list.Count > 0)
        {
            foreach(Enemy nm in list)
            {
                if(nm == null) continue;

                if(nm.Died && !nm.Active)
                {
                    enm = nm;
                    enm.On(pos, rot);
                    break;
                }
            }
        }
        
        if(enm == null)
        {
            enm = Instantiate(enemy);
            enm.On(pos, rot);

            AddEnemy(type, enm);
        }

        return enm;
    }

    public void AddEnemy(EnemyType type, Enemy nm)
    {
        switch(type)
        {
            case EnemyType.Enemy00:
                Enemy00Pool.Add(nm);
                break;
            default:
                break;
        }
    }

    public bool AllDied
    {
        get
        {
            bool allDied = true;
            List<Enemy> enemies = new List<Enemy>();

            enemies.AddRange(Enemy00Pool);
            
            for(int i = 0; i < enemies.Count; i++)
            {
                if(!enemies[i].Died && enemies[i].Active)
                {
                    allDied = false;
                    break;
                }
            }

            return allDied;
        }
    }

    public void ResetPool()
    {

    }
}

[System.Serializable]
public enum EnemyType
{
    Default, Enemy00
}
