using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class DetectionPool : MonoBehaviour
{
    [Inject] public static DetectionPool Instance { get; set; }
    [Inject] void Awake() => Instance = this;

    [System.Serializable]
    public class Pool
    {
        public List<Detection> List = new List<Detection>();
    }

    private int _typeCount = 0;
    public int TypeCount
    {
        get
        {
            if(_typeCount == 0)
            {
                _typeCount = Enum.GetNames(typeof(DetectType)).Length;
            }
            return _typeCount;
        }
    }

    public List<Pool> Pools = new List<Pool>();

    public void CreatePools()
    {
        Pools.Clear();
        for(int i = 0; i < TypeCount; i++)
        {
            Pools.Add(new Pool());
        }
    }

    public void AddInPool(Detection tr, DetectType type)
    {
        if(Pools.Count < TypeCount)
        {
            CreatePools();
        }

        GetListByType(type).Add(tr);
    }

    public void RemoveFromPool(Detection tr, DetectType type)
    {
        if(Pools.Count < TypeCount)
        {
            CreatePools();
        }

        GetListByType(type).Remove(tr);
    }

    public List<Detection> RequirePools(DetectType type)
    {
        List<Detection> list = new List<Detection>();
        DetectType tp = DetectType.Nothing;

        if(type == DetectType.Nothing)
        {
            return list;
        }

        for(int i = 0; i < TypeCount; i++)
        {
            tp = GetTypeByIndex(i);
            if(type.HasFlag(tp))
            {
                list.AddRange(GetListByType(tp));
            }
        }

        return list;
    }

    DetectType GetTypeByIndex(int i)
    {
        int t = (int)(Mathf.Pow(2, i));
        return (DetectType)t;
    }

    int GetIndexByType(DetectType type)
    {
        int i = Convert.ToInt32(type);
        if(i == 0) return -1;

        i = (int)(Mathf.Log(i, 2));
        return i;
    }

    List<Detection> GetListByType(DetectType type)
    {
        if(Pools.Count < TypeCount)
        {
            CreatePools();
        }
        
        return Pools[GetIndexByType(type)].List;
    }
}
