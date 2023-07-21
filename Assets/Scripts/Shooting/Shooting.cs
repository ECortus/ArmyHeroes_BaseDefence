using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public virtual float Damage { get; set; }
    protected virtual float Delay { get; set; }
    protected virtual List<Transform> Muzzles { get; set; }
    protected virtual Ammo Ammo { get; set; }

    [SerializeField] private Target trg;
    public ShootingUpgrades ups;

    public bool isEnable => coroutine != null;
    Coroutine coroutine;

    public virtual void Enable()
    {
        if(coroutine == null)
        {
            coroutine = StartCoroutine(PAWPAW());
            if(delaypoolvar < requirePause) delaypoolvar = requirePause;
        }
    }

    public virtual void Disable()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    public float delaypoolvar { get; set; }
    float requirePause = 0.09f;

    void Update()
    {
        if(delaypoolvar > requirePause)
        {
            delaypoolvar -= Time.deltaTime;
        }
    }

    IEnumerator PAWPAW()
    {
        yield return new WaitForSeconds(delaypoolvar);

        while(true)
        {
            Shoot();
            delaypoolvar += Delay;

            yield return new WaitForSeconds(Delay);
        }
    }

    Ammo ammo = null;
 
    void Shoot()
    {
        Vector3 pos = Vector3.zero;
        Quaternion rot = Quaternion.identity;

        foreach(Transform muzzle in Muzzles)
        {
            muzzle.forward = transform.forward;

            pos = muzzle.position;
            rot = muzzle.rotation;

            if(ups != null)
            {
                if(ups.AmmoPerShotMultiple == 1)
                {
                    Shot(pos, rot);
                }
                else
                {
                    Vector3 mainPos = pos;

                    float spaceBetweenMultiples = 0.2f;
                    float t = -(ups.AmmoPerShotMultiple - 1) / 2f;
                    
                    for(int i = 0; i < ups.AmmoPerShotMultiple; i++)
                    {
                        pos = mainPos + new Vector3(t, 0f, 0f) * spaceBetweenMultiples;
                        t++;

                        Shot(pos, rot);
                    }
                }
            }
            else
            {
                Shot(pos, rot);
            }
        }
    }

    void Shot(Vector3 pos, Quaternion rot)
    {
        ammo = AmmoPool.Instance.Insert(Ammo.Type, Ammo);

        ammo.On(pos, rot, trg.target.position);
        ammo.SetDamage(Damage);

        if(ups.specificAmmoUp != null)
        {
            ammo.SetSpecificEffect(ups.specificAmmoUp);
        }
    }
}
