using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    public static ParticlePool Instance;
    void Awake() => Instance = this;

    private List<ParticleSystem>
        DynamitEffectPool = new List<ParticleSystem>(),
        ElectricHitEffectPool = new List<ParticleSystem>(),
        LongAttackEffectPool = new List<ParticleSystem>();

    public GameObject Insert(ParticleType type, ParticleSystem obj, Vector3 pos, Quaternion rot = new Quaternion())
    {
        List<ParticleSystem> list = new List<ParticleSystem>();

        switch(type)
        {
            case ParticleType.Dynamit:
                list = DynamitEffectPool;
                break;
            case ParticleType.ElectricHit:
                list = ElectricHitEffectPool;
                break;
            case ParticleType.LongAttack:
                list = LongAttackEffectPool;
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
                if(rot != new Quaternion()) ps.transform.rotation = rot;
                ps.Play();
                return ps.gameObject;
            }
        }

        ParticleSystem scr = Instantiate(obj, pos, rot);
        scr.Play();
        list.Add(scr);

        switch(type)
        {
            case ParticleType.Dynamit:
                DynamitEffectPool = list;
                break;
            case ParticleType.ElectricHit:
                ElectricHitEffectPool = list;
                break;
            case ParticleType.LongAttack:
                LongAttackEffectPool = list;
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
    Default, Dynamit, ElectricHit, LongAttack
}
