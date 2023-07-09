using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    public static ParticlePool Instance;
    void Awake() => Instance = this;

    private List<ParticleSystem> BulletEffectPool = new List<ParticleSystem>();

    public GameObject Insert(ParticleType type, GameObject obj, Vector3 pos)
    {
        List<ParticleSystem> list = new List<ParticleSystem>();

        switch(type)
        {
            case ParticleType.Bullet:
                list = BulletEffectPool;
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
            default:
                break;
        }

        return scr.gameObject;
    }
}

[System.Serializable]
public enum ParticleType
{
    Default, Bullet
}
