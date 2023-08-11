using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level", fileName = "lvl00")]
public class LevelWavesInfo : ScriptableObject
{
    public Wave[] Waves = new Wave[0];
    public float DelayBetweenCalls = 1f, DelayBetweenSlots = 1f;

    public int Count => Waves.Length;

    [Header("Rewards: ")]
    public int TokenReward = 100;
}

[System.Serializable]
public class Wave
{
    public Slot[] Slots = new Slot[0];
}

[System.Serializable]
public class Slot
{
    public Call[] Calls = new Call[0];
}

[System.Serializable]
public class Call
{
    public Enemy enemy;
    public int Count;
}
