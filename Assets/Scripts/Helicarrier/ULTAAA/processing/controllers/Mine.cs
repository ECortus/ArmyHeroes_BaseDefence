using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Mine : MonoBehaviour
{
    private float Damage = 0f;
    public void SetDamage(float dmg)
    {
        Damage = dmg;
    }

    [SerializeField] private GameObject model;
    [SerializeField] private ParticleSystem destroy;

    bool Hited = false;
    public bool Active => gameObject.activeSelf;

    public void On()
    {
        Hited = false;

        model.SetActive(true);
        gameObject.SetActive(true);
    }

    public async void Off()
    {
        model.SetActive(false);
        if(destroy != null) destroy.Play();

        await UniTask.Delay(2000);

        gameObject.SetActive(false);
    }

    void Hit(Detection det)
    {
        Hited = true;

        det.GetHit(Damage);
        Off();
    }

    void OnTriggerEnter(Collider col)
    {
        if(Hited) return;

        GameObject go = col.gameObject;

        switch(go.tag)
        {
            case "Enemy":
                Hit(col.GetComponent<Detection>());
                break;
            default:
                break;
        }
    }
}
