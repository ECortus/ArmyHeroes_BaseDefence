using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePool : MonoBehaviour
{
    public static ResourcePool Instance;
    void Awake() => Instance = this;

    [SerializeField] private List<ResourceBall> Balls = new List<ResourceBall>();

    public ResourceBall GetBallByType(ResourceType type)
    {
        ResourceBall rc = null;

        switch(type)
        {
            case ResourceType.Exp:
                rc = Balls[0];
                break;
            case ResourceType.HealKit:
                rc = Balls[1];
                break;
            case ResourceType.Magnit:
                rc = Balls[2];
                break;
            case ResourceType.Dynamit:
                rc = Balls[3];
                break;
            default:
                break;
        }

        return rc;
    }

    private List<ResourceBall> 
        ExpPool = new List<ResourceBall>(), 
        GoldPool = new List<ResourceBall>(), 
        HealKitPool = new List<ResourceBall>(), 
        MagnitPool = new List<ResourceBall>(), 
        DynamitPool = new List<ResourceBall>();

    public ResourceBall Insert(ResourceType type, Vector3 pos = new Vector3())
    {
        ResourceBall rec = null;
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

        if(list.Count > 0)
        {
            foreach(ResourceBall rc in list)
            {
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
}

[System.Serializable]
public enum ResourceType
{
    Exp, Gold, HealKit, Magnit, Dynamit
}
