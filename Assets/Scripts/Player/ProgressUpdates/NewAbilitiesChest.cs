using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class NewAbilitiesChest : MonoBehaviour
{
    bool Opened = false;

    [SerializeField] private Collider col;
    [SerializeField] private Animation spawnAnim, anim;
    [SerializeField] private ParticleSystem particle;
    
    [Space] [SerializeField] private Transform head;

    public bool Active => gameObject.activeSelf;

    public async void On(Vector3 pos)
    {
        col.enabled = false;
        head.eulerAngles = new Vector3(-90f, 0f, 0f);
        
        Opened = false;
        gameObject.SetActive(true);

        transform.position = pos;

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

    async void Open()
    {
        Opened = true;

        anim.Play();
        particle.Play();

        await UniTask.Delay((int)(anim.GetClip("lootOpen").length * 1000));

        PlayerNewProgress.Instance.OnWith3X();
        await UniTask.WaitUntil(() => !PlayerNewProgress.Instance.Active);

        Off();
    }
}
