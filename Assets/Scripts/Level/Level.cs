using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks;

public class Level : MonoBehaviour
{
    public void On() => gameObject.SetActive(true);
    public void Off() => gameObject.SetActive(false);
    public void Eliminate() => Destroy(gameObject);
    public void StartLevel()
    {
        GameManager.Instance.SetActive(true);
    }
    public void EndLevel()
    {
        UI.Instance.EndLevel();



        GameManager.Instance.SetActive(false);
    }
} 
