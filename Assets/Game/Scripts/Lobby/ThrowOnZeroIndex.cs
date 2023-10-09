using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowOnZeroIndex : MonoBehaviour
{
    [SerializeField] private LoadGameManager loading;
    
    void Start()
    {
        if (Statistics.LevelIndex == 0)
        {
            loading.LoadGame();
        }
    }
}
