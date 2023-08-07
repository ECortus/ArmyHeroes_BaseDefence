using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinPlaySetter : MonoBehaviour
{
    [SerializeField] private GameObject[] skinsMeshes;

    void Start()
    {
        for(int i = 0; i < skinsMeshes.Length; i++)
        {
            if(i == PlayerSkin.Index)
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
