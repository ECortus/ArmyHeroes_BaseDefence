using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class DefeatedScenes : MonoBehaviour
{
    [SerializeField] private Transform playerDeathCamera;
    
    [Space]
    [SerializeField] private Transform helicarrierDeathCamera;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private GameObject mainHeli, brokenHeli;

    public void PlayerDefeat()
    {
        Time.timeScale = 0.1f;
        playerDeathCamera.position = new Vector3(
            playerDeathCamera.parent.position.x,
            playerDeathCamera.position.y, 
            playerDeathCamera.parent.position.z + 2f
        );
        CameraController.Instance.SetTarget(playerDeathCamera);

        ReviveUI.Instance.On();
    }

    public async void HelicarrierDestroyed()
    {
        Time.timeScale = 0.1f;
        explosion.Play();
        
        helicarrierDeathCamera.transform.position = new Vector3(
            helicarrierDeathCamera.position.x, 
            helicarrierDeathCamera.position.y, 
            helicarrierDeathCamera.position.z - 3f
        );
        CameraController.Instance.SetTarget(helicarrierDeathCamera);
        
        await UniTask.Delay((int)(explosion.totalTime * 750), DelayType.UnscaledDeltaTime);
        
        mainHeli.SetActive(false);
        brokenHeli.SetActive(true);
        await UniTask.Delay(1000, DelayType.UnscaledDeltaTime);

        ReviveUI.Instance.On();
    }
}
