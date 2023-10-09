using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class LandScene : MonoBehaviour
{
    public static LandScene Instance { get; set; }
    void Awake() => Instance = this;

    [SerializeField] private CameraController cmr;
    [SerializeField] private Player player;
    [SerializeField] private PlayerExitFromHeli exiting;

    [Space]
    [SerializeField] private Animation heliAnim;
    [SerializeField] private Transform spawnDot;

    [Space] [SerializeField] private GameObject recycle;
    [SerializeField] private GameObject hpbar;

    void Start()
    {
        On();
    }

    public void On()
    {
        hpbar.SetActive(false);
        
        gameObject.SetActive(true);
        StartCoroutine(Process());
    }

    public async void Off()
    {
        await exiting.Exit();
        
        gameObject.SetActive(false);

        cmr.SetTarget(player.Transform);
        player.On(spawnDot.position, Quaternion.Euler(0f, -90f, 0f));

        hpbar.SetActive(true);
        if(Statistics.LevelIndex > 0) recycle?.SetActive(true);
        OnStartUI.Instance.On();
    }

    IEnumerator Process()
    {
        player.Off();

        cmr.SetTarget(spawnDot);
        cmr.Reset();

        heliAnim.Play("HeliLand");

        yield return new WaitForSeconds(heliAnim.GetClip("HeliLand").length);
        Off();
    }
}
