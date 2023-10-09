using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Detection : Health
{   
    private DetectionPool Pools => DetectionPool.Instance;
    [Space]
    public DetectType Type;
    
    public bool Marked => MarkedBy != null;
    private Detection MarkedBy;

    public Detection GetMarkedBy() => MarkedBy;

    public void SetMarkedBy(Detection mark)
    {
        MarkedBy = mark;
    }

    public void ResetMarkedBy()
    {
        MarkedBy = null;
    }

    public void Pool()
    {
        DetectionPool.Instance.AddInPool(this, Type);
        MarkedBy = null;
    }

    public void Depool()
    {
        DetectionPool.Instance.RemoveFromPool(this, Type);
        MarkedBy = null;
    }

    public bool AllAround(DetectType priority, DetectType essential, float prioriryDistance, float essentialDistance, IAdditionalCondition ac, out Detection[] data)
    {
        Detection[] sorted = Pools.RequirePools(priority);

        if(sorted.Length== 0 || !SortDetectionListByDistance(sorted, prioriryDistance, ac, out sorted))
        {
            sorted = Pools.RequirePools(essential);
            if(sorted.Length == 0 || !SortDetectionListByDistance(sorted, essentialDistance, ac, out sorted))
            {
                data = new Detection[0];
                return false;
            }
        }

        data = sorted;
        return sorted.Length > 0;
    }

    public bool NearestAround(DetectType priority, DetectType essential, float prioriryDistance, float essentialDistance, IAdditionalCondition ac, out Detection data)
    {
        Detection unit = null;
        Detection[] detectCols = new Detection[0];

        if(AllAround(priority, essential, prioriryDistance, essentialDistance, ac, out detectCols))
        {
            unit = detectCols[0];
        }

        data = unit;
        return data != null;
    }

    bool SortDetectionListByDistance(Detection[] nonSorted, float distance, IAdditionalCondition ac, out Detection[] sorted)
    {
        Detection detection = null;

        if(nonSorted.Length == 0)
        {
            sorted = new Detection[0];
            return false;
        }

        List<Detection> sort = new List<Detection>(nonSorted);

        if(sort.Contains(this))
        {
            sort.Remove(this);
        }

        List<Detection> nonActiveSort = new List<Detection>(sort);

        for(int i = 0; i < nonActiveSort.Count; i++) /// additional check on died/active etc...
        {
            detection = nonActiveSort[i];
            if((ac == null ? !detection.Active : !ac.AdditionalCondition(detection)))
            {
                sort.Remove(detection);
            }
        }

        sort = sort.OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).ToList();

        List<Detection> sortByDistance = new List<Detection>();

        for(int i = 0; i < sort.Count; i++)
        {
            detection = sort[i];
            if(Vector3.Distance(transform.position, detection.transform.position) <= distance)
            {
                sortByDistance.Add(detection);
            }
            else
            {
                break;
            }
        }

        sorted = sortByDistance.ToArray();
        return sortByDistance.Count > 0;
    }
}
