using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class NewAbilitiesChest : MonoBehaviour
{
    bool Opened = false;

    public bool Active => gameObject.activeSelf;

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

        PlayerNewProgress.Instance.OnWith3X();
        await UniTask.WaitUntil(() => !PlayerNewProgress.Instance.Active);

        Off();
    }
}
