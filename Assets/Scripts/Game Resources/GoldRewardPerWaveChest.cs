using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class GoldRewardPerWaveChest : ResourceDrop
{
    public static GoldRewardPerWaveChest Instance { get; set; }
    void Awake() => Instance = this;

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
