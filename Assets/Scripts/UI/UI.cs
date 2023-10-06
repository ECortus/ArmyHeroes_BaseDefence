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

    [Space] [SerializeField] private GameObject pauseObject;

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

    public void PauseOn()
    {
        Time.timeScale = 0f;
        pauseObject.SetActive(true);
    }

    public void PauseOff()
    {
        Time.timeScale = 1f;
        pauseObject.SetActive(false);
    }
}