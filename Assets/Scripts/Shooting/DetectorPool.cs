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
    public Transform Player;
    public List<Transform> Allies = new List<Transform>();
    public List<Transform> Firepositions = new List<Transform>();
    public List<Transform> Buildings = new List<Transform>();
    public List<Transform> Enemies = new List<Transform>();
    public List<Transform> Crystals = new List<Transform>();

    public void AddInPool(Transform tr, DetectType type)
    {
        List<Transform> list = GetListByType(type);
        list.Add(tr);
    }

    public void RemoveFromPool(Transform tr, DetectType type)
    {
        List<Transform> list = GetListByType(type);
        list.Remove(tr);
    }

    public List<Transform> RequirePools(DetectType type)
    {
        List<Transform> list = new List<Transform>();
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

    public bool CheckTypeInDetectTypes(DetectType type)
    {
        DetectType tp = DetectType.Nothing;

        for(int i = 0; i < TypeCount; i++)
        {
            tp = GetTypeByIndex(i);
            if(type.HasFlag(tp))
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
                return DetectType.Fireposition;
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

    List<Transform> GetListByType(DetectType type)
    {
        switch(type)
        {
            case DetectType.Player:
                return new List<Transform>{Player};
            case DetectType.Ally:
                return Allies;
            case DetectType.Fireposition:
                return Firepositions;
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