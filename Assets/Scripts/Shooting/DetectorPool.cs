using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class DetectorPool : MonoBehaviour
{
    [Inject] public static DetectorPool Instance { get; set; }
    [Inject] void Awake() => Instance = this;

    public int TypeCount = 6;
    public List<Info> Player = new List<Info>();
    public List<Info> Allies = new List<Info>();
    public List<Info> Helicarriers = new List<Info>();
    public List<Info> Buildings = new List<Info>();
    public List<Info> Enemies = new List<Info>();
    public List<Info> Crystals = new List<Info>();

    public void AddInPool(Info tr, DetectType type)
    {
        List<Info> list = GetListByType(type);
        list.Add(tr);
    }

    public void RemoveFromPool(Info tr, DetectType type)
    {
        List<Info> list = GetListByType(type);
        list.Remove(tr);
    }

    public List<Info> RequirePools(DetectType type)
    {
        List<Info> list = new List<Info>();
        DetectType tp = DetectType.Nothing;

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

    public bool CheckTypeInDetectTypes(DetectType type, DetectType types)
    {
        DetectType tp = DetectType.Nothing;

        for(int i = 0; i < TypeCount; i++)
        {
            tp = GetTypeByIndex(i);
            if(types.HasFlag(tp))
            {
                
            }
        }

        return false;
    }

    DetectType GetTypeByIndex(int i)
    {
        switch(i)
        {
            case 0:
                return DetectType.Player;
            case 1:
                return DetectType.Ally;
            case 2:
                return DetectType.Helicarrier;
            case 3:
                return DetectType.Building;
            case 4:
                return DetectType.Enemy;
            case 5:
                return DetectType.Crystal;
            default:
                return DetectType.Nothing;
        }
    }

    List<Info> GetListByType(DetectType type)
    {
        switch(type)
        {
            case DetectType.Player:
                return Player;
            case DetectType.Ally:
                return Allies;
            case DetectType.Helicarrier:
                return Helicarriers;
            case DetectType.Building:
                return Buildings;
            case DetectType.Enemy:
                return Enemies;
            case DetectType.Crystal:
                return Crystals;
            default:
                return null;
        }
    }
}