using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Detection : Health
{   
    private DetectionPool Pools => DetectionPool.Instance;
    public DetectType Type;
    public bool Marked;

    public void Pool()
    {
        DetectionPool.Instance.AddInPool(this, Type);
        Marked = false;
    }

    public void Depool()
    {
        DetectionPool.Instance.RemoveFromPool(this, Type);
        Marked = false;
    }

    public bool AllAround(DetectType priority, DetectType essential, float prioriryDistance, float essentialDistance, IAdditionalCondition ac, out List<Detection> data)
    {
        List<Detection> sorted = new List<Detection>(Pools.RequirePools(priority));

        if(sorted.Count == 0 || !SortDetectionListByDistance(sorted, prioriryDistance, ac, out sorted))
        {
            sorted = new List<Detection>(Pools.RequirePools(essential));
            if(sorted.Count == 0 || !SortDetectionListByDistance(sorted, essentialDistance, ac, out sorted))
            {
                data = new List<Detection>();
                return false;
            }
        }

        data = sorted;
        return sorted.Count > 0;
    }

    public bool NearestAround(DetectType priority, DetectType essential, float prioriryDistance, float essentialDistance, IAdditionalCondition ac, out Detection data)
    {
        Detection unit = null;
        List<Detection> detectCols = new List<Detection>();

        if(AllAround(priority, essential, prioriryDistance, essentialDistance, ac, out detectCols))
        {
            unit = detectCols[0];
        }

        data = unit;
        return data != null;
    }

    public bool SortDetectionListByDistance(List<Detection> nonSorted, float distance, IAdditionalCondition ac, out List<Detection> sorted)
    {
        if(nonSorted.Count == 0)
        {
            sorted = new List<Detection>();
            return false;
        }

        List<Detection> sort = new List<Detection>(nonSorted);

        if(sort.Contains(this))
        {
            sort.Remove(this);
        }

        List<Detection> nonActiveSort = new List<Detection>(sort);
        foreach(Detection det in nonActiveSort) /// additional check on died/active etc...
        {
            if((ac == null ? !det.Active : !ac.AdditionalCondition(det)))
            {
                sort.Remove(det);
            }
        }

        sort = sort.OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).ToList();

        List<Detection> sortByDistance = new List<Detection>();
        foreach(Detection det in sort)
        {
            if(Vector3.Distance(transform.position, det.transform.position) <= distance)
            {
                sortByDistance.Add(det);
            }
            else
            {
                break;
            }
        }

        sorted = sortByDistance;
        return sortByDistance.Count > 0;
    }
}
