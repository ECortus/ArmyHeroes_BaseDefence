using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public virtual float Damage { get; set; }
    protected virtual float Delay { get; set; }
    protected virtual List<Transform> Muzzles { get; set; }
    protected virtual Ammo Ammo { get; set; }

    public bool isEnable => coroutine != null;
    Coroutine coroutine;
    WaitForSeconds delay;

    public void Enable()
    {
        if(coroutine == null)
        {
            coroutine = StartCoroutine(PAWPAW());
            delay = new WaitForSeconds(Delay);
        }
    }

    public void Disable()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    IEnumerator PAWPAW()
    {
        yield return new WaitForSeconds(0.1f);

        while(true)
        {
            Shoot();
            yield return delay;
        }
    }
 
    void Shoot()
    {
        Vector3 pos = Vector3.zero;
        Quaternion rot = Quaternion.identity;
        Ammo ammo = null;

        foreach(Transform muzzle in Muzzles)
        {
            pos = muzzle.position;
            rot = muzzle.rotation;

            ammo = AmmoPool.Instance.Insert(AmmoType.Bullet, Ammo, pos, rot);
            ammo.SetDamage(Damage);
        }
    }
}
