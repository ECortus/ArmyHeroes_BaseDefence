using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("DEBUG: ")]
    [SerializeField] float _HP;

    [Header("HP Input: ")]
    [SerializeField] private float InputHP = 25f;
    private  float Bonus = 0f;

    [Header("HP UI ref-s: ")]
    [SerializeField] private HealthBarUI bar;

    [Header("HP Events: ")]
    [SerializeField] private  UnityEvent ResurrectEvent;
    [SerializeField] private  UnityEvent DeathEvent;

    public bool Active => gameObject.activeSelf;
    public bool Died { get; set; }

    public float HP
    {
        get
        {
            return _HP;
        }
        set
        {
            _HP = value;
            if(bar != null)
            {
                bar.data = this;
                bar.Refresh();
            }
        }
    }

    public float MaxHP => InputHP * (1f + Bonus);

    public virtual void Heal(float mnt)
    {
        HP += mnt;
        if(HP > MaxHP) HP = MaxHP;
    }
    
    public virtual void GetHit(float mnt)
    {
        HP -= mnt;
        if(HP <= 0f && !Died)
        {
            Death();
        }
    }

    public void AddBonus(float percent)
    {
        Bonus += percent;
    }

    public void ResetBonus()
    {
        Bonus = 0f;
    }

    public virtual void Resurrect()
    {
        Died = false;
        Heal(9999f);

        ResurrectEvent?.Invoke();
    }

    public virtual void Death()
    {
        Died = true;
        HP = 0f;

        DeathEvent?.Invoke();
    }
}
