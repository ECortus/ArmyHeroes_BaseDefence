using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Info : MonoBehaviour
{
    [Header("UI ref-s: ")]
    [SerializeField] private HealthBarUI bar;

    [Header("Up-s:")]
    [SerializeField] private InfoUpgrades ups;

    [Header("Events: ")]
    [SerializeField] private UnityEvent DeathEvent;
    [SerializeField] private UnityEvent ResurrectEvent;

    public virtual float InputMaxHealth { get; set; }
    public virtual float InputDamage { get; set; }

    public virtual float MaxHealth => InputMaxHealth * (1f + ups.PlusHealth / 100f);
    public virtual float Damage => InputDamage * (1f + ups.PlusDamage / 100f);

    public bool Died { get; set; }

    float _health;
    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            if(bar != null) bar.Refresh();
        }
    }

    public virtual void Heal(float mnt)
    {
        Health += mnt;
        if(Health > MaxHealth) Health = MaxHealth;
    }
    
    public virtual void GetHit(float mnt)
    {
        Health -= mnt;
        if(Health <= 0f)
        {
            Death();
        }
    }

    public virtual void Resurrect()
    {
        ResurrectEvent?.Invoke();
    }

    public virtual void Death()
    {
        DeathEvent?.Invoke();
    }

    public virtual void Hit(Info nf)
    {
        nf.GetHit(Damage);
    }
}
