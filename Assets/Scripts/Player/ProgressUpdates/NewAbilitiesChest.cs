using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class NewAbilitiesChest : MonoBehaviour
{
    bool Opened = false;

    [SerializeField] private Animation anim;
    [SerializeField] private ParticleSystem particle, spawnpart;
    
    [Space] [SerializeField] private Transform head;

    private bool Available = false;
    public bool Active => gameObject.activeSelf;

    public async void On(Vector3 pos)
    {
        Available = false;
        head.localEulerAngles = new Vector3(-90f, 0f, 0f);
        
        Opened = false;
        gameObject.SetActive(true);

        transform.position = pos + new Vector3(0, 0.5f, 0);

        anim.Play("chestSpawn");

        float del = anim.GetClip("chestSpawn").length;
        await UniTask.Delay((int)(del * 1000));
        
        spawnpart.Play();
        Available = true;
    }

    public void Off()
    {
        anim.Stop();
        particle.Stop();

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

    async void Open()
    {
        Opened = true;

        anim.Play("lootOpen");
        particle.Play();

        await UniTask.Delay((int)(anim.GetClip("lootOpen").length * 1000));

        PlayerNewProgress.Instance.OnWith3X();
        await UniTask.WaitUntil(() => !PlayerNewProgress.Instance.Active);

        Off();
    }
}
