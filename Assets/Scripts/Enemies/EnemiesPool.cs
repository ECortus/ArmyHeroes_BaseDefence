using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPool : MonoBehaviour
{
    public static EnemiesPool Instance;
    void Awake() => Instance = this;

    public Enemy Insert(EnemyType type, Enemy enemy, Vector3 pos = new Vector3(), Quaternion rot = new Quaternion())
    {
        Enemy enm = null;
        List<Enemy> list = new List<Enemy>();

        switch(type)
        {
            default:
                break;
        }

        if(list.Count > 0)
        {
            foreach(Enemy nm in list)
            {
                if(nm == null) continue;

                if(!nm.Active)
                {
                    enm = nm;
                    enm.On();
                    break;
                }
            }
        }
        
        if(enm == null)
        {
            enm = Instantiate(enemy, pos, Quaternion.identity);
            enm.On();

            AddRecource(type, enm);
        }

        return enm;
    }

    public void AddRecource(EnemyType type, Enemy nm)
    {
        switch(type)
        {
            default:
                break;
        }
    }

    public void ResetPool()
    {

    }
}

[System.Serializable]
public enum EnemyType
{
    Default
}
