using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAbilitiesChestSpawner : MonoBehaviour
{
    [SerializeField] private NewAbilitiesChest[] newAbilitiesChests;
    [SerializeField] private float secondsDelay = 30f;

    private float time = 0f;
    private NewAbilitiesChest ActiveChest = null;

    public void SpawnOneRandom()
    {
        SpawnOneByIndex(Random.Range(0, newAbilitiesChests.Length));
    }

    public void SpawnOneByIndex(int index)
    {
        ActiveChest = newAbilitiesChests[index];
        ActiveChest.On();
    }

    void Start()
    {
        time = secondsDelay;
    }

    void Update()
    {
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
            SpawnOneRandom();
            time = secondsDelay;
        }
    }
}
