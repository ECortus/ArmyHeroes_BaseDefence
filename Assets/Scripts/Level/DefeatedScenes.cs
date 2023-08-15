using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class DefeatedScenes : MonoBehaviour
{
    [SerializeField] private Transform playerDeathCamera;
    [SerializeField] private Transform helicarrierDeathCamera;

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

    public void HelicarrierDestroyed()
    {
        Time.timeScale = 0.1f;
        helicarrierDeathCamera.transform.position = new Vector3(
            helicarrierDeathCamera.position.x, 
            helicarrierDeathCamera.position.y, 
            helicarrierDeathCamera.position.z + 6f
        );
        CameraController.Instance.SetTarget(helicarrierDeathCamera);

        ReviveUI.Instance.On();
    }
}
