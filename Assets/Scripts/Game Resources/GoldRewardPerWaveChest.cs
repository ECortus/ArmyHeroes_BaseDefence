using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class GoldRewardPerWaveChest : ResourceDrop
{
    [SerializeField] private Collider col;
    [SerializeField] private Animation spawnAnim, anim;
    [SerializeField] private ParticleSystem particle;

    [Space] [SerializeField] private Transform head;

    bool Opened = false;

    void Start()
    {
        OpenedIndex = CurrentIndex;
        Off();
    }

    public async void On()
    {
        head.eulerAngles = Vector3.zero;
        col.enabled = false;
        
        Opened = false;
        gameObject.SetActive(true);

        /*spawnAnim.Play();
        
        await UniTask.Delay(1000);*/
        col.enabled = true;
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

    private int OpenedIndex = 0;
    private int CurrentIndex => EndLevelStats.Instance.WaveIndex;

    async void Open()
    {
        Opened = true;
        int count = BallAmount * (CurrentIndex - OpenedIndex);

        anim.Play();
        particle.Play();

        await UniTask.Delay((int)(anim.GetClip("chestOpen").length * 1000));

        while(count > 0)
        {
            ResourcePerBallMod = WaveRewardLVLs.WCRMod;
            DropAmount(1);
            await UniTask.Delay(50);

            count--;
        }

        OpenedIndex = CurrentIndex;
        Off();
    }
}
