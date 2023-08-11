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
    private List<Detection> targets;

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
        targets = DetectionPool.Instance.RequirePools(TargetTypes).ToList();
        index = Random.Range(0, targets.Count);
        target = targets[index];

        point = target.transform.position;
        target.GetHit(Damage);
        targets.Remove(target);

        effect.Play();
        effect.transform.position = new Vector3(point.x, 0.75f, point.y);

        targets = targets.OrderBy(x => Vector3.Distance(point, x.transform.position)).ToList();

        foreach(Detection trg in targets)
        {
            if((trg.transform.position - point).magnitude > Range)
            {
                break;
            }

            trg.GetHit(Damage / 2f);
        }

        CameraShake.Instance.On(1f);
    }
    
    public override IEnumerator Deprocess()
    {
        anim.Play("bounceSpawnReverse");
        yield return null;
    }
}
