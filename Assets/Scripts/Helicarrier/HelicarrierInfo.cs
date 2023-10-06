using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicarrierInfo : Detection
{
    private Player Player => Player.Instance;
    
    [Space]
    [SerializeField] private PlayerMech PlayerMech;
    [SerializeField] private float mechDelay = 30f;
    
    void Start()
    {
        Resurrect();
    }

    private bool playerMechCoroutineOned;

    public override void GetHit(float mnt)
    {
        if (PlayerMech.gameObject.activeSelf) return;
        
        base.GetHit(mnt);

        if (HP / MaxHP <= 0.5f && !Died && !playerMechCoroutineOned)
        {
            StartMech();
        }
    }

    public void StartMech()
    {
        playerMechCoroutineOned = true;
        StartCoroutine(Mech());
    }

    IEnumerator Mech()
    {
        Player.Off();
        PlayerMech.On(Player.Transform.position);
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
    }
}
