using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicarrierInfo : Detection
{
    private Player Player => Player.Instance;
    
    [Space]
    [SerializeField] private PlayerMech PlayerMech;
    [SerializeField] private float mechDelay = 30f;
    [SerializeField] private float delayToShowPopUpAgain = 60f;
    private float delayTime = 60f;

    private Coroutine _coroutine;
    
    void Start()
    {
        Resurrect();
        delayTime = delayToShowPopUpAgain * 2f;
    }

    void Update()
    {
        delayTime -= Time.deltaTime;
    }

    public override void GetHit(float mnt)
    {
        if (PlayerMech.gameObject.activeSelf) return;
        
        base.GetHit(mnt);

        if (HP / MaxHP <= 0.5f && !Died && delayTime <= 0f && _coroutine == null)
        {
            RewardPopUpController.Instance.ShowMechPopUp();
            delayTime = delayToShowPopUpAgain;
        }
    }

    public void StartMech()
    {
        _coroutine = StartCoroutine(Mech());
    }

    [Space] 
    [SerializeField] private float minDistanceToSpawnMech = 8;

    IEnumerator Mech()
    {
        Player.Off();

        Vector3 spawnPos = Player.Transform.position;
        if (Vector3.Distance(transform.position, spawnPos) < minDistanceToSpawnMech)
        {
            Vector3 dir = (spawnPos - transform.position).normalized;
            spawnPos = transform.position + dir * minDistanceToSpawnMech + Vector3.up;
        }
        
        PlayerMech.On(spawnPos);
        CameraController.Instance.SetTarget(PlayerMech.Transform);
        
        float time = mechDelay;

        while (time > 0f)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        
        PlayerMech.Off();
        Player.On(PlayerMech.Transform.position);
        CameraController.Instance.SetTarget(Player.Transform);
        
        delayTime = delayToShowPopUpAgain;

        _coroutine = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, minDistanceToSpawnMech);
    }
}
