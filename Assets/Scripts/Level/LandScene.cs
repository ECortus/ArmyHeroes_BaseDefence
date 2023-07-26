using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandScene : MonoBehaviour
{
    [SerializeField] private CameraController cmr;
    [SerializeField] private Player player;

    [Space]
    [SerializeField] private Animation heliAnim;
    [SerializeField] private Transform spawnDot;

    void Start()
    {
        On();
    }

    public void On()
    {
        StartCoroutine(Process());
    }

    public void Off()
    {
        gameObject.SetActive(false);

        cmr.SetTarget(player.Transform);
        player.On(spawnDot.position, Quaternion.Euler(0f, -90f, 0f));

        LevelManager.Instance.ActualLevel.StartLevel();
    }

    IEnumerator Process()
    {
        player.Off();

        cmr.SetTarget(spawnDot);
        heliAnim.Play();

        yield return new WaitForSeconds(heliAnim.GetClip("HeliLand").length);
        Off();
    }
}
