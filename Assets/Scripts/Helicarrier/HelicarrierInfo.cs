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

    private Coroutine playerMechCoroutine;

    public override void GetHit(float mnt)
    {
        base.GetHit(mnt);

        if (HP / MaxHP <= 0.5f)
        {
            StartMech();
        }
    }

    public void StartMech()
    {
        playerMechCoroutine ??= StartCoroutine(Mech());
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
