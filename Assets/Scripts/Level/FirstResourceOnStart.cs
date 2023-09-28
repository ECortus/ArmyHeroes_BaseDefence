using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstResourceOnStart : MonoBehaviour
{
    [SerializeField] private Transform expDot;
    [SerializeField] private int ExpAmount;
    [SerializeField] private float ExpPerBall;

    [Space] [SerializeField] private Transform goldDot;
    [SerializeField] private int GoldAmount;
    [SerializeField] private float GoldPerBall;

    [Space] [SerializeField] private float spaceInRow;
    [SerializeField] private float spaceInColumn;

    public bool HaveToSpawn
    {
        get
        {
            return PlayerPrefs.GetInt("SpawnResourceOnStart", 0) <= 0;
        }
        set
        {
            PlayerPrefs.SetInt("SpawnResourceOnStart", value ? 0 : 1);
            PlayerPrefs.Save();
        }
    }

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        if (Statistics.LevelIndex <= 0 || !HaveToSpawn) return;
        DropResources();
    }
    
    void DropResources()
    {
        Drop(ExpAmount, ResourceType.Exp, expDot.position, ExpPerBall);
        Drop(GoldAmount, ResourceType.Gold, goldDot.position, GoldPerBall);

        HaveToSpawn = false;
    }

    void Drop(int amount, ResourceType type, Vector3 center, float resAmount)
    {
        ResourceBall ball = null;
        Vector3 pos;
        
        int size = 0;
        size = (int)(amount / Mathf.Pow(amount, 0.5f));

        int column = 0;
        int row = 0;
        
        for (int i = 0; i < amount; i++)
        {
            column = i % size - size / 2 - 1/2 * (size % 2 > 0 ? 1 : 0);
            row = i / size - size / 2 - 1/2 * (size % 2 > 0 ? 1 : 0);
            pos = center + new Vector3(spaceInColumn * column, 0, spaceInRow * row);
            
            ball = ResourcePool.Instance.Insert(type, pos);
            ball.SetRecource(resAmount);
        }
    }
}
