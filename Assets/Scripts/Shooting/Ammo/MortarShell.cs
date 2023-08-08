using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarShell : Ammo
{
    private float Damage => GetDamage();
    public override AmmoType Type => AmmoType.Rocket;

    [Space]
    [SerializeField] private ParticleSystem standartHitEffect;
    [SerializeField] private float blowRadius;

    Vector3 destination;
    float distance;
    float g;

    float G = 9.81f;

    public override void On(Vector3 spawn, Quaternion rot, Vector3 destination = new Vector3())
    {
        base.On(spawn, rot);

        distance = Vector3.Distance(Center, destination);
        transform.eulerAngles += new Vector3(-30f, 0f, 0f);

        g = G;
        g *= Mathf.Pow(speed, 2) / (distance * G) * 1.2f;

        rb.velocity = transform.forward * speed;
    }

    public override void Off(Transform pos = null)
    {
        List<Enemy> enemies = EnemiesPool.Instance.GetAllEnemiesOnDistance(Center, blowRadius);

        foreach(Enemy enemy in enemies)
        {
            if(enemy.Active && !enemy.Died)
            {
                enemy.detection.GetHit(Damage);
                enemy.ForceBack(250f);
            }
        }

        if(standartHitEffect != null)
        {
            standartHitEffect.Play();
        }

        base.Off();
    }

    protected override void Update()
    {
        base.Update();
        
        if(!Hited)
        {
            rb.velocity -= new Vector3(0f, g * Time.deltaTime, 0f);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        GameObject go = col.gameObject;

        switch(go.tag)
        {
            case "Enemy":
                OnHit(col.GetComponent<Detection>());
                break;
            case "Ground":
                Off();
                break;
            case "Building":
                Off();
                break;
            default:
                break;
        }
    }
}
