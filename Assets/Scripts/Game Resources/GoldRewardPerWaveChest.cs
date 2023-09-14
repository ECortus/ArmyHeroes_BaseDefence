using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class GoldRewardPerWaveChest : ResourceDrop
{
    [SerializeField] private Animation anim;
    [SerializeField] private ParticleSystem particle, spawnpart;

    [Space] [SerializeField] private Transform head;
    private TutorialArrow Arrow => LevelManager.Instance.TutorArrow;

    private bool Available = false;
    bool Opened = false;

    void Start()
    {
        OpenedIndex = CurrentIndex;
        Off();
    }

    public async void On()
    {
        Available = false;
        head.localEulerAngles = new Vector3(-90f, 0f, 0f);
        
        Opened = false;
        gameObject.SetActive(true);

        anim.Play("chestSpawn");
        
        await UniTask.Delay((int)(anim.GetClip("chestSpawn").length * 1000));
        
        spawnpart.Play();
        Available = true;

        Arrow.On();
        Arrow.SetTarget(transform);
    }

    public void Off()
    {
        anim.Stop();
        particle.Stop();
        
        Arrow.Off();

        gameObject.SetActive(false);
    }

    void OnTriggerStay(Collider col)
    {
        if(Opened || !Available) return;
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
        int count = BallAmount;

        anim.Play("lootOpen");
        particle.Play();

        float del = anim.GetClip("lootOpen").length;
        await UniTask.Delay((int)(del * 1000));

        while(count > 0)
        {
            ResourcePerBallMod = WaveRewardLVLs.WCRMod + Mathf.Clamp(CurrentIndex - OpenedIndex, 0, 999);
            DropAmount(1);
            await UniTask.Delay(40);

            count--;
        }

        OpenedIndex = CurrentIndex;
        Off();
    }
}
