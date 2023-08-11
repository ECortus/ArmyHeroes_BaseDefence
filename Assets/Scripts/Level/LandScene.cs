using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandScene : MonoBehaviour
{
    public static LandScene Instance { get; set; }
    void Awake() => Instance = this;

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
        gameObject.SetActive(true);
        StartCoroutine(Process());
    }

    public void Off()
    {
        gameObject.SetActive(false);

        cmr.SetTarget(player.Transform);
        player.On(spawnDot.position, Quaternion.Euler(0f, -90f, 0f));

        OnStartUI.Instance.On();
    }

    IEnumerator Process()
    {
        player.Off();

        cmr.SetTarget(spawnDot);
        cmr.Reset();

        heliAnim.Play();

        yield return new WaitForSeconds(heliAnim.GetClip("HeliLand").length);
        Off();
    }
}
