using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OrbHitProcessing : UltProcessing_IEnumerator
{
    [SerializeField] private UltOrbHit info;
    [SerializeField] private Animation anim;
    [SerializeField] private ParticleSystem effect;

    private float Damage => info.Damage;
    private float Range => info.Range;

    private DetectType TargetTypes = DetectType.Enemy | DetectType.Boss;
    private Detection[] targets;

    Detection target;
    Vector3 point;
    int index;

    public override IEnumerator Process()
    {
        yield return null;
        anim.Play("bounceSpawn");

        int count = info.ShootCount;

        while(count > 0)
        {
            Shoot();
            yield return new WaitForSeconds(info.Duration / info.ShootCount);

            count--;
        }
    }

    void Shoot()
    {
        targets = DetectionPool.Instance.RequirePools(TargetTypes);
        index = Random.Range(0, targets.Length);
        target = targets[index];

        point = target.transform.position;
        target.GetHit(Damage);
        targets = targets[1..targets.Length];
        
        effect.Stop();
        effect.transform.position = new Vector3(point.x, 0.75f, point.z);
        /*Debug.Log($"Orb hit: {point}");*/
        effect.Play();

        /*targets = targets.OrderBy(x => Vector3.Distance(point, x.transform.position)).ToArray();*/

        foreach(Detection trg in targets)
        {
            if((trg.transform.position - point).magnitude > Range)
            {
                break;
            }

            trg.GetHit(Damage / 2f);
        }

        CameraShake.Instance.On(0.6f);
    }
    
    public override IEnumerator Deprocess()
    {
        anim.Play("bounceSpawnReverse");
        yield return null;
    }
}
