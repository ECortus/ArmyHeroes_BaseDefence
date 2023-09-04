using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAbilitiesChestSpawner : MonoBehaviour
{
    [SerializeField] private NewAbilitiesChest[] newAbilitiesChests;
    [SerializeField] private float secondsDelay = 30f;

    private float time = 0f;
    private NewAbilitiesChest ActiveChest = null;

    public Transform SpawnOneRandom()
    {
        return SpawnOneByIndex(Random.Range(0, newAbilitiesChests.Length));
    }

    public Transform SpawnOneByIndex(int index)
    {
        ActiveChest = newAbilitiesChests[index];
        ActiveChest.On(Player.Instance.transform.position + Vector3.forward);

        return ActiveChest.transform;
    }

    void Start()
    {
        time = secondsDelay;
    }

    void Update()
    {
        if (Statistics.LevelIndex <= 0) return;
        
        if(ActiveChest != null)
        {
            if(ActiveChest.Active) return;
        }

        if(time > 0f)
        {
            time -= Time.deltaTime;
        }
        else
        {
            SpawnOneByIndex(0);
            time = secondsDelay;
        }
    }
}
