using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ProduceHitOfSpecificAmmo : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private Detection detection;
    private HP_DMG_SPD spd => detection.HPvDMGvSPD;

    [Space] [SerializeField] private ParticleSystem[] Effects;

    Coroutine poison, fire, freeze;
    private float poisonTime, fireTime, freezeTime;

    public void TurnOnPoison(float damage, float time)
    {
        poisonTime = time;
        if(poison == null)
        {
            poison = StartCoroutine(Poisoning(damage));
        }
    }

    IEnumerator Poisoning(float dmg)
    {
        Effects[0].gameObject.SetActive(true);
        Effects[0].Play();

        while(poisonTime > 0f)
        {
            if(detection.Died || !detection.Active)
            {
                break;
            }

            detection.GetHit(dmg);
            yield return new WaitForSeconds(1f);

            poisonTime -= 1f;
        }

        Effects[0].Stop();
        Effects[0].gameObject.SetActive(false);
        poison = null;
    }

    public void TurnOnFire(float damage, float time)
    {
        fireTime = time;
        if(fire == null)
        {
            fire = StartCoroutine(Firing(damage));
        }
    }

    IEnumerator Firing(float dmg)
    {
        Effects[1].gameObject.SetActive(true);
        Effects[1].Play();

        while(fireTime > 0f)
        {
            if(detection.Died || !detection.Active)
            {
                break;
            }

            detection.GetHit(dmg);
            yield return new WaitForSeconds(1f);

            fireTime -= 1f;
        }

        Effects[1].Stop();
        Effects[1].gameObject.SetActive(false);
        
        fire = null;
    }

    public void TurnOnElectric(float damage, float maxDistanceBetweenTargets, int maxTargetsCount)
    {
        DetectType Types = DetectType.Enemy | DetectType.Boss;
        List<Detection> enemies = DetectionPool.Instance.RequirePools(Types).ToList();

        enemies = enemies.OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).ToList();

        DamageByElectric(detection, damage);
        if(enemies.Contains(detection))
        {
            enemies.Remove(detection);
        }

        Transform current = transform;

        for(int i = 0; i < maxTargetsCount; i++)
        {
            if(enemies.Count == 0)
            {
                break;
            }

            if((enemies[0].transform.position - current.position).magnitude < maxDistanceBetweenTargets)
            {
                if (!enemies[0].Died && enemies[0].Active)
                {
                    DamageByElectric(enemies[0], damage);
                }
                
                enemies.Remove(enemies[0]);
                if(enemies.Count > 0)
                {
                    current = enemies[0].transform;
                }
            }
        }
    }

    void DamageByElectric(Detection target, float dmg)
    {
        target.GetHit(dmg);

        ParticleSystem effect = Effects[2];
        
        /*effect.gameObject.SetActive(true);*/
        ParticlePool.Instance.Insert(ParticleType.ElectricHit, effect, target.transform.position);
    }

    public void TurnOnFreeze(float decreaseOnPercent, float time)
    {
        freezeTime = time;
        if(freeze == null)
        {
            freeze = StartCoroutine(Freezing(decreaseOnPercent));
        }
    }

    IEnumerator Freezing(float decrease)
    {
        Effects[3].gameObject.SetActive(true);
        Effects[3].Play();

        detection.HPvDMGvSPD.AddSPDPercent(-decrease);

        while(freezeTime > 0f)
        {
            if(detection.Died || !detection.Active)
            {
                break;
            }

            freezeTime -= Time.deltaTime;
            yield return null;
        }

        detection.HPvDMGvSPD.ResetSPD();

        Effects[3].Stop();
        Effects[3].gameObject.SetActive(true);
        
        freeze = null;
    }

    public void TurnOnImpulse(float force)
    {
        /*Effects[4].gameObject.SetActive(true);*/
        
        if (detection.Type.HasFlag(DetectType.Enemy))
        {
            Effects[4].Play();
            enemy.ForceBack(force);
        }
    }
}
