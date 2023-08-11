using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class GoldRewardPerWaveChest : ResourceDrop
{
    public static GoldRewardPerWaveChest Instance { get; set; }
    void Awake() => Instance = this;

    [SerializeField] private Animation anim;
    [SerializeField] private ParticleSystem particle;

    bool Opened = false;

    void Start()
    {
        Off();
    }

    public void On()
    {
        Opened = false;
        gameObject.SetActive(true);
    }

    public void Off()
    {
        anim.Stop();
        particle.Stop();

        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider col)
    {
        if(Opened) return;
        GameObject go = col.gameObject;

        switch(go.tag)
        {
            case "Player":
                Open();
                break;
            default:
                break;
        }
    }

    async void Open()
    {
        Opened = true;
        int count = BallAmount;

        anim.Play();
        particle.Play();

        await UniTask.Delay((int)(anim.GetClip("chestOpen").length * 1000));

        while(count > 0)
        {
            ResourcePerBallMod = 1f + WaveRewardLVLs.WCRMod;
            DropAmount(1);
            await UniTask.Delay(50);

            count--;
        }

        Off();
    }
}
