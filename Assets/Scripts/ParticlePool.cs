using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    public static ParticlePool Instance;
    void Awake() => Instance = this;

    private List<ParticleSystem> 
        BulletEffectPool = new List<ParticleSystem>(),
        DynamitEffectPool = new List<ParticleSystem>(),
        RocketEffectPool = new List<ParticleSystem>(),
        SubBuckshotEffectPool = new List<ParticleSystem>(),
        ArrowEffectPool = new List<ParticleSystem>();

    public GameObject Insert(ParticleType type, GameObject obj, Vector3 pos)
    {
        List<ParticleSystem> list = new List<ParticleSystem>();

        switch(type)
        {
            case ParticleType.Bullet:
                list = BulletEffectPool;
                break;
            case ParticleType.Dynamit:
                list = DynamitEffectPool;
                break;
            case ParticleType.Rocket:
                list = RocketEffectPool;
                break;
            case ParticleType.SubBuckshot:
                list = SubBuckshotEffectPool;
                break;
            case ParticleType.Arrow:
                list = ArrowEffectPool;
                break;
            default:
                break;
        }

        foreach(ParticleSystem ps in list)
        {
            if(ps == null) continue;

            if(!ps.isPlaying) 
            {
                ps.transform.position = pos;
                ps.Play();
                return ps.gameObject;
            }
        }

        ParticleSystem scr = Instantiate(obj, pos, Quaternion.Euler(Vector3.zero)).GetComponent<ParticleSystem>();
        list.Add(scr);

        switch(type)
        {
            case ParticleType.Bullet:
                BulletEffectPool = list;
                break;
            case ParticleType.Dynamit:
                DynamitEffectPool = list;
                break;
            case ParticleType.Rocket:
                RocketEffectPool = list;
                break;
            case ParticleType.SubBuckshot:
                SubBuckshotEffectPool = list;
                break;
            case ParticleType.Arrow:
                ArrowEffectPool = list;
                break;
            default:
                break;
        }

        return scr.gameObject;
    }
}

[System.Serializable]
public enum ParticleType
{
    Default, Bullet, SubBuckshot, Arrow, Rocket, Dynamit
}
