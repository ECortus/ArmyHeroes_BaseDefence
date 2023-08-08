using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;

public class Fireposition : Detection
{
    public bool CallSomeone => calledUser != null;
    public bool Busy => user != null;

    FirepositionUser user;
    FirepositionUser calledUser;

    [Space]
    [SerializeField] private float putInRange;
    [SerializeField] private Transform userSpawnDot;

    [Space]
    public FirepositionUpgrader upgrader;

    public override float MaxHP 
    { 
        get
        {
            return InputHP * (1 + upgrader.Progress + BuildingUpgradesLVLs.HPMod);
        }
    }

    Vector3 Center
    {
        get
        {
            return transform.position;
        }
    }

    void Start()
    {
        On();
    }

    public void On()
    {
        gameObject.SetActive(true);

        Resurrect();
        Pool();

        FirepositionsOperator.Instance.PoolFireposition(this);

        RefreshModel();
    }

    public override void Heal(float mnt)
    {
        base.Heal(mnt);
        RefreshModel();
    }

    public override void GetHit(float mnt)
    {
        base.GetHit(mnt);
        RefreshModel();
    }

    public override void Death()
    {
        base.Death();
        if(Busy) PutOutUser();

        FirepositionsOperator.Instance.DepoolFireposition(this);
    }

    public void Off()
    {
        gameObject.SetActive(false);
        Depool();

        FirepositionsOperator.Instance.DepoolFireposition(this);
    }

    public async void CallUser(FirepositionUser cu)
    {
        calledUser = cu;
        
        if(calledUser != null && user == null)
        {
            calledUser.Call(transform);

            await UniTask.WaitUntil(() => (Center - calledUser.transform.position).magnitude < putInRange);
            PutInUser(calledUser);
        }
    }

    public void PutInUser(FirepositionUser sr)
    {
        user = sr;
        user.Off();

        calledUser = null;

        EnableAction();
    }

    public void PutOutUser()
    {
        user.On(userSpawnDot.position);

        /* if(Died)
        {
            user.GetHit(999f);
        } */

        user = null;
        DisableAction();
    }

    protected virtual void EnableAction() { }
    protected virtual void DisableAction() { }
    public virtual void RefreshModel() { }
}
