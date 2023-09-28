using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePool : MonoBehaviour
{
    public static ResourcePool Instance;
    void Awake() => Instance = this;

    [SerializeField] private List<int> EachResourceLimit = new List<int>();
    [SerializeField] private List<ResourceBall> Balls = new List<ResourceBall>();

    public ResourceBall GetBallByType(ResourceType type)
    {
        ResourceBall rc = null;

        switch(type)
        {
            case ResourceType.Exp:
                rc = Balls[0];
                break;
            case ResourceType.Gold:
                rc = Balls[1];
                break;
            case ResourceType.HealKit:
                rc = Balls[2];
                break;
            case ResourceType.Magnit:
                rc = Balls[3];
                break;
            case ResourceType.Dynamit:
                rc = Balls[4];
                break;
            default:
                break;
        }

        return rc;
    }
    
    private List<ResourceBall> 
        ActiveExpPool = new List<ResourceBall>(), 
        ActiveGoldPool = new List<ResourceBall>(), 
        ActiveHealKitPool = new List<ResourceBall>(), 
        ActiveMagnitPool = new List<ResourceBall>(), 
        ActiveDynamitPool = new List<ResourceBall>();

    private List<ResourceBall> 
        ExpPool = new List<ResourceBall>(), 
        GoldPool = new List<ResourceBall>(), 
        HealKitPool = new List<ResourceBall>(), 
        MagnitPool = new List<ResourceBall>(), 
        DynamitPool = new List<ResourceBall>();

    public void SetMoveAllType(ResourceType type)
    {
        List<ResourceBall> list = new List<ResourceBall>();

        switch(type)
        {
            case ResourceType.Exp:
                list = ExpPool;
                break;
            case ResourceType.Gold:
                list = GoldPool;
                break;
            case ResourceType.HealKit:
                list = HealKitPool;
                break;
            case ResourceType.Magnit:
                list = MagnitPool;
                break;
            case ResourceType.Dynamit:
                list = DynamitPool;
                break;
            default:
                break;
        }

        for(int i = 0; i < list.Count; i++)
        {
            if(list[i].Active)
            {
                list[i].SetMove(true);
            }
        }
    }

    public ResourceBall Insert(ResourceType type, Vector3 pos = new Vector3())
    {
        ResourceBall rec = null;
        List<ResourceBall> list = new List<ResourceBall>();
        List<ResourceBall> activeList = new List<ResourceBall>();

        int limit = 0;

        switch(type)
        {
            case ResourceType.Exp:
                list = ExpPool;
                limit = EachResourceLimit[0];
                activeList = ActiveExpPool;
                break;
            case ResourceType.Gold:
                list = GoldPool;
                limit = EachResourceLimit[1];
                activeList = ActiveGoldPool;
                break;
            case ResourceType.HealKit:
                list = HealKitPool;
                limit = EachResourceLimit[2];
                activeList = ActiveHealKitPool;
                break;
            case ResourceType.Magnit:
                list = MagnitPool;
                limit = EachResourceLimit[3];
                activeList = ActiveMagnitPool;
                break;
            case ResourceType.Dynamit:
                list = DynamitPool;
                limit = EachResourceLimit[4];
                activeList = ActiveDynamitPool;
                break;
            default:
                break;
        }

        if(list.Count > 0 && activeList.Count < limit)
        {
            ResourceBall rc;
            
            for(int i = 0; i < list.Count; i++)
            {
                rc = list[i];
                
                if(rc == null) continue;

                if(!rc.Active)
                {
                    rec = rc;
                    rec.On(pos);
                    break;
                }
            }
        }
        
        if(rec == null)
        {
            rec = Instantiate(GetBallByType(type), pos, Quaternion.identity);
            rec.On(pos);

            AddRecource(type, rec);
        }

        return rec;
    }
    
    public void AddActiveRecource(ResourceType type, ResourceBall rc)
    {
        switch(type)
        {
            case ResourceType.Exp:
                ActiveExpPool.Add(rc);
                break;
            case ResourceType.Gold:
                ActiveGoldPool.Add(rc);
                break;
            case ResourceType.HealKit:
                ActiveHealKitPool.Add(rc);
                break;
            case ResourceType.Magnit:
                ActiveMagnitPool.Add(rc);
                break;
            case ResourceType.Dynamit:
                ActiveDynamitPool.Add(rc);
                break;
            default:
                break;
        }
    }
    
    public void RemoveActiveRecource(ResourceType type, ResourceBall rc)
    {
        switch(type)
        {
            case ResourceType.Exp:
                ActiveExpPool.Remove(rc);
                break;
            case ResourceType.Gold:
                ActiveGoldPool.Remove(rc);
                break;
            case ResourceType.HealKit:
                ActiveHealKitPool.Remove(rc);
                break;
            case ResourceType.Magnit:
                ActiveMagnitPool.Remove(rc);
                break;
            case ResourceType.Dynamit:
                ActiveDynamitPool.Remove(rc);
                break;
            default:
                break;
        }
    }

    public void AddRecource(ResourceType type, ResourceBall rc)
    {
        switch(type)
        {
            case ResourceType.Exp:
                ExpPool.Add(rc);
                break;
            case ResourceType.Gold:
                GoldPool.Add(rc);
                break;
            case ResourceType.HealKit:
                HealKitPool.Add(rc);
                break;
            case ResourceType.Magnit:
                MagnitPool.Add(rc);
                break;
            case ResourceType.Dynamit:
                DynamitPool.Add(rc);
                break;
            default:
                break;
        }
    }

    public void ResetPool()
    {
        ExpPool.Clear();
        GoldPool.Clear();
        HealKitPool.Clear();
        MagnitPool.Clear();
        DynamitPool.Clear();
    }

    void Update()
    {
        for (int i = 0; i < ActiveGoldPool.Count; i++)
        {
            Rotate(ActiveGoldPool[i].transform);
        }
        
        for (int i = 0; i < ActiveDynamitPool.Count; i++)
        {
            Rotate(ActiveGoldPool[i].transform);
        }
        
        for (int i = 0; i < ActiveMagnitPool.Count; i++)
        {
            Rotate(ActiveGoldPool[i].transform);
        }
        
        for (int i = 0; i < ActiveHealKitPool.Count; i++)
        {
            Rotate(ActiveGoldPool[i].transform);
        }
    }

    void Rotate(Transform rot)
    {
        rot.rotation = Quaternion.Euler(rot.eulerAngles + new Vector3(0f, 105f * Time.deltaTime, 0f));
    }
}

[System.Serializable]
public enum ResourceType
{
    Exp, Gold, HealKit, Magnit, Dynamit
}
