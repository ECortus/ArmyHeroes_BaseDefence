using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameManager : MonoBehaviour, ILoading
{
    [SerializeField] private LoadingScreen loadingScreen;

    public void LoadGame()
    {
        loadingScreen.LoadScene(this);
    }

    public AsyncOperation LoadFunction()
    {
        return SceneManager.LoadSceneAsync("PLAY", LoadSceneMode.Single);
    }
}
