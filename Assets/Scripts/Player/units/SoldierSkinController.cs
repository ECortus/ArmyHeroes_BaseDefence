using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierSkinController : MonoBehaviour
{
    [SerializeField] private UpgradeSoldiers upgrades;
    [SerializeField] private GameObject[] skinsMeshes;

    private int index => upgrades.UsedCount >= skinsMeshes.Length ? skinsMeshes.Length - 1 : upgrades.UsedCount;

    void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        for(int i = 0; i < skinsMeshes.Length; i++)
        {
            if(i == index)
            {
                skinsMeshes[i].SetActive(true);
            }
            else
            {
                skinsMeshes[i].SetActive(false);
            }
        }
    }
}
