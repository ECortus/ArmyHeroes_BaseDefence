using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLobbyManager : MonoBehaviour, ILoading
{
    [SerializeField] private LoadingScreen loadingScreen;

    public void LoadLobby()
    {
        loadingScreen.LoadScene(this);
        Time.timeScale = 1f;
    }
    
    public AsyncOperation LoadFunction()
    {
        return SceneManager.LoadSceneAsync("LOBBY", LoadSceneMode.Single);
    }
}
