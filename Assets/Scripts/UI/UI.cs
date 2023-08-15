using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using TMPro;

public class UI : MonoBehaviour
{
    public static UI Instance { get; set; }

    [SerializeField] private OpenCloseObjectLevelUI End, Lose;
    [SerializeField] private LoadLobbyManager loadLobbyManager;

    void Awake()
    {
        Instance = this;
    }

    public void On()
    {

    }

    public void Off()
    {

    }

    public void LoadLobby()
    {
        loadLobbyManager.LoadLobby();
    }

    public void EndLevel()
    {
        End.Open();
    }

    public void LoseLevel()
    {
        Lose.Open();
    }
}