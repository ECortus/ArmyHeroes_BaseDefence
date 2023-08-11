using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class DefeatedScenes : MonoBehaviour
{
    [SerializeField] private Transform playerDeathCamera;
    [SerializeField] private Transform helicarrierDeathCamera;

    public async void PlayerDefeat()
    {
        Time.timeScale = 0.5f;
        CameraController.Instance.SetTarget(playerDeathCamera);

        await UniTask.Delay(2500);

        UI.Instance.LoseLevel();
    }

    public async void HelicarrierDestroyed()
    {
        Time.timeScale = 0.5f;
        CameraController.Instance.SetTarget(helicarrierDeathCamera);

        await UniTask.Delay(2500);

        UI.Instance.LoseLevel();
    }
}
