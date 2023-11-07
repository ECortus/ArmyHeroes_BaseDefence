using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class BossSpecialAttacker : MonoBehaviour
{
    [SerializeField] private Enemy controller;
    
    private NavMeshAgent Agent => controller.Agent;
    private Animator Animator => controller.Animator;

    [SerializeField] private Animation attackAnim;
    
    [Header("Long attack: ")]
    [SerializeField] private string longAttackAnimName;
    [SerializeField] private ParticleSystem longEffect;
    [SerializeField] private AttackHitBox longHitBox;
    
    [Header("Run attack: ")]
    [SerializeField] private string runAttackAnimName;

    [SerializeField] private ParticleSystem runEffect;
    [SerializeField] private AttackHitBox runHitBox;
    [SerializeField] private float runTime = 1;
    [SerializeField] private float runSpeed = 15;
    
    [Space]
    [SerializeField] private float minDelay = 5f;
    [SerializeField] private float maxDelay = 15f;

    [Space] [SerializeField] private UnityEvent stopEvent;
    [SerializeField] private UnityEvent continueEvent;

    [Space] [SerializeField] private int unitSpawnAroundCount = 5;
    [SerializeField] private float distanceFromBossCenter = 2f;
    [SerializeField] private Enemy unitSpawnPrefab;

    float time = 0;
    void SetTime()
    {
        time = Random.Range(minDelay, maxDelay);
    }

    private Coroutine attack;
    private int randomNum => Random.Range(0, 100);

    void Start()
    {
        SetTime();
    }
    
    void Update()
    {
        if (!GameManager.Instance.isActive) return;

        if (!controller.Agent.isOnNavMesh) return;
        
        time -= Time.deltaTime;
        if (time < 0f && attack == null)
        {
            /*if (randomNum > 50)*/
            {
                StartLongAttack();
            }
            /*else
            {
                StartRunAttack();
            }*/
        }
    }

    public void StartLongAttack()
    {
        if(attack == null) stopEvent?.Invoke();
        attack ??= StartCoroutine(LongAttack());
    }

    public void StartRunAttack()
    {
        if(attack == null) stopEvent?.Invoke();
        attack ??= StartCoroutine(RunAttack());
    }

    public void StopAttack()
    {
        if (attack != null)
        {
            StopCoroutine(attack);
            attack = null;
            
            continueEvent?.Invoke();
        }
        
        controller.takeControl = false;
        longHitBox.Off();
        runHitBox.Off();
    }
    
    public void SpawnUnitsAround()
    {
        EnemiesGenerator gen = LevelManager.Instance.ActualLevel.Generator;
        Vector3 center = transform.position;
        Vector3 dir = Vector3.zero;
        float angle = 0f;
        
        for(int i = 0; i < unitSpawnAroundCount; i++)
        {
            angle = 360f / unitSpawnAroundCount * i;
            dir = DirectionFromAngle(transform.eulerAngles.y, angle);

            gen.Spawn(unitSpawnPrefab, center + dir * distanceFromBossCenter);
        }
    }

    IEnumerator LongAttack()
    {
        float time = attackAnim.GetClip(longAttackAnimName).length;

        controller.takeControl = true;
        
        longHitBox.On();
        attackAnim.Play(longAttackAnimName);
        
        yield return new WaitForSeconds(time - 0.25f);
        time = 0.25f;

        ParticlePool.Instance.Insert(ParticleType.LongAttack, longEffect, longHitBox.transform.position, longHitBox.transform.rotation);
        
        while (time > 0f)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        
        attackAnim.Stop();
        longHitBox.Off();
        
        SetTime();
        StopAttack();
    }

    IEnumerator RunAttack()
    {
        float time = runTime;
        
        runHitBox.On();
        attackAnim.Play(runAttackAnimName);

        Vector3 direction;
        
        yield return new WaitForSeconds(attackAnim.GetClip(runAttackAnimName).length);
        runEffect.Play();
        
        while (time > 0f)
        {
            direction = (controller.target.transform.position - transform.position).normalized;
            direction.y = 0;
                
            Agent.Move(direction * runSpeed * Time.deltaTime);
            
            time -= Time.deltaTime;
            yield return null;
        }
        
        runEffect.Stop();
        attackAnim.Stop();
        runHitBox.Off();
        
        SetTime();
        StopAttack();
    }
    
    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
