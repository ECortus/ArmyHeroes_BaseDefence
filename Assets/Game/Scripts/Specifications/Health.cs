using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("DEBUG: ")]
    [SerializeField] float _HP;

    [Header("HP Input: ")]
    public float InputHP = 25f;
    public HP_DMG_SPD HPvDMGvSPD;

    [Header("HP UI ref-s: ")]
    [SerializeField] HealthBarUI bar;

    [Header("HP Events: ")]
    public UnityEvent ResurrectEvent;
    [SerializeField] private UnityEvent DeathEvent;

    [Header("Misc ref-s:")] public EmojiesController EmojiesController;

    public bool Active => gameObject.activeSelf;
    public bool Died => HP <= 0f;

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

    public virtual float MaxHP 
    { 
        get
        {
            if(HPvDMGvSPD == null)
            {
                return InputHP;
            }

            return InputHP * (1f + HPvDMGvSPD.bonusHP / 100f);
        }
    }

    public virtual void Heal(float mnt)
    {
        if(HP <= 0 && mnt > 0f)
        {
            ResurrectEvent?.Invoke();
        }

        HP += mnt;
        if(HP > MaxHP) HP = MaxHP;
    }
    
    public virtual void GetHit(float mnt)
    {
        if(HP <= 0f) return;
        
        if(EmojiesController != null) EmojiesController.PlayWounded();

        HP -= mnt;
        if(HP <= 0f)
        {
            HP = 0f;
            Death();
        }
    }

    public virtual void Resurrect()
    {
        Heal(999999f);

        if(bar != null) bar.gameObject.SetActive(true);
        ResurrectEvent?.Invoke();
    }

    public virtual void Death()
    {
        if(bar != null) bar.gameObject.SetActive(false);
        DeathEvent?.Invoke();
    }
}
