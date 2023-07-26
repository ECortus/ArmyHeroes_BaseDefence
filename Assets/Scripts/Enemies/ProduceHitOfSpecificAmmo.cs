using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ProduceHitOfSpecificAmmo : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private Detection detection;
    private HP_DMG_SPD spd => detection.HPvDMGvSPD;

    [Space]
    [SerializeField] private List<ParticleSystem> Effects = new List<ParticleSystem>();

    Coroutine poison, fire, freeze;

    public void TurnOnPoison(float damage, float time)
    {
        if(poison == null)
        {
            poison = StartCoroutine(Poisoning(damage, time));
        }
    }

    IEnumerator Poisoning(float dmg, float duration)
    {
        float time = duration;
        Effects[0].Play();

        while(time > 0f)
        {
            if(detection.Died || !detection.Active)
            {
                break;
            }

            detection.GetHit(dmg);
            yield return new WaitForSeconds(1f);

            time -= 1f;
        }

        Effects[0].Stop();
        poison = null;
    }

    public void TurnOnFire(float damage, float time)
    {
        if(fire == null)
        {
            fire = StartCoroutine(Firing(damage, time));
        }
    }

    IEnumerator Firing(float dmg, float duration)
    {
        float time = duration;
        Effects[1].Play();

        while(time > 0f)
        {
            if(detection.Died || !detection.Active)
            {
                break;
            }

            detection.GetHit(dmg);
            yield return new WaitForSeconds(1f);

            time -= 1f;
        }

        Effects[1].Stop();
        fire = null;
    }

    public void TurnOnElectric(float damage, float maxDistanceBetweenTargets, int maxTargetsCount)
    {
        DetectType Types = DetectType.Enemy | DetectType.Boss;
        List<Detection> enemies = DetectionPool.Instance.RequirePools(Types);

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
                DamageByElectric(enemies[0], damage);
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
        ParticlePool.Instance.Insert(ParticleType.ElectricHit, effect, target.transform.position);
    }

    public void TurnOnFreeze(float decreaseOnPercent, float time)
    {
        if(freeze == null)
        {
            freeze = StartCoroutine(Freezing(decreaseOnPercent, time));
        }
    }

    IEnumerator Freezing(float decrease, float duration)
    {
        float time = duration;
        Effects[3].Play();

        detection.HPvDMGvSPD.AddSPDPercent(-decrease);

        while(time > 0f)
        {
            if(detection.Died || !detection.Active)
            {
                break;
            }

            time -= Time.deltaTime;
            yield return null;
        }

        detection.HPvDMGvSPD.ResetSPD();

        Effects[3].Stop();
        freeze = null;
    }

    public void TurnOnImpulse(float force)
    {
        Effects[4].Play();
        enemy.ForceBack(force);
    }
}
