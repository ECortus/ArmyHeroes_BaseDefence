using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform Transform => transform;

    public bool AutoShooting = false;

    [Space]
    [SerializeField] private float _Damage;

    public void SetDefaultDamage(float dmg)
    {
        _Damage = dmg;
    }

    [SerializeField] private float _Delay;
    [SerializeField] private Transform Muzzle;
    [SerializeField] private Ammo Ammo;

    [Space] [SerializeField] private bool MultipleOn = true;

    public int AmmoMultiple
    {
        get
        {
            if(ups != null && MultipleOn)
            {
                return ups.AmmoPerShotMultiple;
            }
            else
            {
                return 1;
            }
        }
    }

    public float Damage
    {
        get
        {
            float dmg = _Damage;
            if(hds != null)
            {
                dmg *= (1f + hds.bonusDMG / 100f);
            }
            return dmg;
        }
    }

    public float Delay
    {
        get
        {
            float del = _Delay;
            if(ups != null)
            {
                /*Debug.Log($"Decrease value: {ups.DecreaseSC}");*/
                del *= ups.DecreaseSC;
            }
            return del;
        }
    }

    [Space]
    [SerializeField] private GunHandler Handler;
    private Target trg
    {
        get
        {
            return Handler.Target;
        }
    }
    private HP_DMG_SPD hds
    {
        get
        {
            return Handler.HP_DMG_SPD;
        }
    }
    private ShootingUpgrades ups
    {
        get
        {
            return Handler.ShootingUpgrades;
        }
    }

    [Space]
    [SerializeField] private ParticleSystem AdditionalEffectOnShooting;

    public bool isEnable => coroutine != null;
    Coroutine coroutine;

    void Start()
    {
        if(AutoShooting)
        {
            Enable();
        }
    }

    public virtual void Enable()
    {
        if(!isEnable)
        {
            if(AdditionalEffectOnShooting != null) AdditionalEffectOnShooting.Play();

            coroutine = StartCoroutine(PAWPAW());

            if(delaypoolvar < requirePause) 
            {
                delaypoolvar = requirePause;

                if(AutoShooting)
                {
                    if(delaypoolvar < requirePause + Delay)
                    {
                        delaypoolvar += Delay;
                    }
                }
            }
        }
    }

    public virtual void Disable()
    {
        if(isEnable)
        {
            if(AdditionalEffectOnShooting != null) AdditionalEffectOnShooting.Stop();

            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    float delaypoolvar;
    float requirePause = 0.09f;

    void Update()
    {
        /* transform.localPosition = Vector3.zero; */

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
            if(AutoShooting)
            {
                if(trg.Transform == trg.target)
                {
                    yield return new WaitUntil(() => trg.Transform != trg.target);
                }
            }

            Shoot();
            delaypoolvar += Delay;

            yield return new WaitForSeconds(Delay);
        }
    }

    Vector3 GetCorrectTargetPoint(Vector3 target)
    {
        Vector3 point = target;
        /* point.y += 0.5f; */
        return point;
    }
 
    void Shoot()
    {
        Vector3 pos = Vector3.zero;
        Quaternion rot = Quaternion.identity;

        /* Muzzle.forward = (GetCorrectTargetPoint(trg.target.position) - trg.Transform.position).normalized; */
        /* Muzzle.forward = trg.Transform.forward; */
        /* transform.forward = (trg.target.position - trg.Transform.position).normalized; */

        pos = Muzzle.position;
        rot = Muzzle.rotation;

        if(AmmoMultiple == 1)
        {
            Shot(Ammo, Damage, pos, rot);
        }
        else
        {
            Vector3 mainPos = pos;

            float spaceBetweenMultiples = 0.2f;
            float t = -(AmmoMultiple - 1) / 2f;
            
            for(int i = 0; i < AmmoMultiple; i++)
            {
                pos = mainPos + new Vector3(t, 0f, 0f) * spaceBetweenMultiples;
                t++;

                Shot(Ammo, Damage, pos, rot);
            }
        }
    }

    void Shot(Ammo ammo, float dmg, Vector3 pos, Quaternion rot)
    {
        ammo = AmmoPool.Instance.Insert(ammo.Type, ammo);

        ammo.On(pos, rot, GetCorrectTargetPoint(trg.target.position));
        ammo.SetDamage(dmg);
        
        if(ups != null)
        {
            ammo.SetSpecifics(ups.Specifics);
        }
    }
}
