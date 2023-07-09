using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private TrailRenderer trial;
    [SerializeField] private float speed;

    private float damage;
    public void SetDamage(float dmg)
    {
        damage = dmg;
    }
    public float GetDamage() => damage;

    public bool Hited { get; set; }

    public void On(Vector3 spawn, Quaternion rot)
    {
        Hited = false;
        trial.Clear();

        gameObject.SetActive(true);

        spawnPos = spawn;
        transform.position = spawn;
        transform.rotation = rot;

        rb.velocity = transform.forward * speed;
    }

    public void Off()
    {
        Hited = true;

        gameObject.SetActive(false);
        EffectOnOff();
    }   

    Vector3 spawnPos;

    public bool FarAwayFromSpawn
    {
        get
        {
            return Vector3.Distance(transform.position, spawnPos) > 100f;
        }
    }

    void Update()
    {
        if(FarAwayFromSpawn)
        {
            Off();
        }
    }

    protected virtual void EffectOnOff()
    {

    }

    protected virtual void OnTriggerEnter(Collider col)
    {

    }
}
