using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Ammo : MonoBehaviour
{
    public virtual AmmoType Type { get; }

    public Rigidbody rb;
    [SerializeField] private SphereCollider sphere;
    [SerializeField] private GameObject ammoModel;

    [Space]
    public float speed;
    [SerializeField] private TrailRenderer trial;

    [Space]
    [SerializeField] private SpecificAmmoArray[] SpecificInfos;

    [HideInInspector] public SpecificType Specifics = SpecificType.Simple;
    public void SetSpecifics(SpecificType tp)
    {
        Specifics = tp;
    }

    private float damage;
    public void SetDamage(float dmg)
    {
        damage = dmg;
    }
    public float GetDamage() => damage;

    public bool Hited => !ammoModel.activeSelf;
    public bool Active => gameObject.activeSelf;

    public void SetSpecificEffect(SpecificType types)
    {

    }   

    public virtual void On(Vector3 spawn, Quaternion rot, Vector3 destination = new Vector3())
    {
        trial.Clear();
        rb.isKinematic = false;

        ammoModel.SetActive(true);
        sphere.enabled = true;

        gameObject.SetActive(true);

        spawnPos = spawn;
        transform.position = spawn;
        transform.rotation = rot;
    }

    public virtual async void Off()
    {
        ammoModel.SetActive(false);
        sphere.enabled = false;

        transform.position = transform.position - transform.forward * 2f;

        rb.velocity = Vector3.zero;
        rb.isKinematic = true;

        await UniTask.Delay(2000);

        gameObject.SetActive(false);
    }   

    Vector3 spawnPos;

    public bool FarAwayFromSpawn
    {
        get
        {
            return Vector3.Distance(Center, spawnPos) > 100f;
        }
    }

    protected virtual void Update()
    {
        if(!Hited)
        {
            if(FarAwayFromSpawn)
            {
                Off();
            }

            transform.forward = rb.velocity.normalized;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    public Vector3 Center
    {
        get
        {
            return transform.TransformPoint(sphere.center);
        }
    }

    public virtual void OnHit(Detection det)
    {
        Off();

        det.GetHit(GetDamage());
        EffectOnHit(det);
    }

    void EffectOnOn()
    {
        foreach(SpecificAmmoArray array in SpecificInfos)
        {
            if(Specifics.HasFlag(array.Type))
            {
                array.OnEffect.Play();
            }
        }
    }

    void EffectOnHit(Detection det)
    {
        foreach(SpecificAmmoArray array in SpecificInfos)
        {
            if(Specifics.HasFlag(array.Type))
            {
                array.OnEffect.Stop();
                array.HitEffect.Play();

                array.OnHit.Action(det);
            }
        }
    }
}

[System.Serializable]
public class SpecificAmmoArray
{
    public SpecificType Type;
    public ParticleSystem OnEffect;
    public ParticleSystem HitEffect;
    public SpecificAmmoOnHitEffect OnHit;
}
