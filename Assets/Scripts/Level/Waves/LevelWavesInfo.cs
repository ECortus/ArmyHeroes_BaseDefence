using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level", fileName = "lvl00")]
public class LevelWavesInfo : ScriptableObject
{
    public List<Wave> Waves = new List<Wave>(); 
    public float DelayBetweenCalls = 1f, DelayBetweenSlots = 1f;
}

[System.Serializable]
public class Wave
{
    public List<Slot> Slots = new List<Slot>();
}

[System.Serializable]
public class Slot
{
    public List<Call> Calls = new List<Call>();
}

[System.Serializable]
public class Call
{
    public Enemy enemy;
    public int Count;
}
