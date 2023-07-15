using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Detector : MonoBehaviour
{
    public float detectRange;
    private DetectorPool Pool => DetectorPool.Instance;

    public DetectType PriorityTypes;
    public DetectType DetectTypes;
/* 
    public LayerMask priorityMask;
    public LayerMask enemiesMask; */

    [HideInInspector] public Info data;
    public virtual HumanoidController humanController { get; set; }
    [SerializeField] private bool CheckCollisionWithTarget = false;
    public bool InColWithTargetMask { get; set; }

    [HideInInspector] public List<Info> DetectedCols = new List<Info>();

    Info previous;
    float distanceToData;
    Coroutine coroutine;

    void Start()
    {
        Enable();
    }

    public void Enable()
    {
        if(coroutine == null)
        {
            coroutine = StartCoroutine(Working());
            data = null;
        }
    }

    public void Disable()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    IEnumerator Working()
    {
        while(true)
        {
            yield return null;

            if(data == null)
            {
                NearestAround(detectRange, out data);
                previous = data;

                if(data != null)
                {
                    Set();
                }
            }

            if(data != null)
            {
                distanceToData = Vector3.Distance(data.transform.position, transform.position);

                if(data.Died || !data.Active || distanceToData > detectRange)
                {
                    Reset();
                    continue;
                }

                if(NearestAround(distanceToData, out data))
                {
                    if(data != previous)
                    {
                        Change();
                        previous = data;
                    }
                }
            }
        }
    }

    protected virtual void Reset()
    {

    }

    protected virtual void Change()
    {

    }

    protected virtual void Set()
    {

    }

    bool NearestAround(float dstnc, out Info data)
    {
        Info unit = null;

        /* List<Collider> cols = Physics.OverlapSphere(transform.position, detectRange, priorityMask).ToList();

        if(cols.Count == 0)
        {
            cols = Physics.OverlapSphere(transform.position, dstnc, enemiesMask).ToList();
        } */

        List<Info> priorityCols = new List<Info>(Pool.RequirePools(PriorityTypes));
        List<Info> detectCols = new List<Info>(Pool.RequirePools(DetectTypes));

        int count = priorityCols.Count + detectCols.Count;

        List<Info> valid = new List<Info>();
        valid.AddRange(priorityCols);

        foreach(Info pr in detectCols)
        {
            if(!valid.Contains(pr))
            {
                valid.Add(pr);
            }
        }

        if(valid.Contains(GetComponentInChildren<Info>()))
        {
            valid.Remove(GetComponentInChildren<Info>());
        }

        detectCols.Clear();
        bool priorityInRange = false;

        foreach(Info col in valid)
        {
            if(priorityCols.Contains(col) || priorityInRange)
            {
                if(Vector3.Distance(transform.position, col.transform.position) <= detectRange)
                {
                    if(!priorityInRange) detectCols.Clear();

                    priorityInRange = true;
                    detectCols.Add(col);
                }
            }
            else
            {
                if(Vector3.Distance(transform.position, col.transform.position) <= dstnc)
                {
                    detectCols.Add(col);
                }
            }
        }

        if(detectCols.Count > 0)
        {
            detectCols = detectCols.OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).ToList();
            
            DetectedCols.Clear();
            DetectedCols.AddRange(detectCols);
            
            for(int i = 0; i < DetectedCols.Count; i++)
            {
                unit = detectCols[i];
                if(!unit.Died && unit.Active)
                {
                    break;
                }
            }
        }

        data = unit;

        if(data != null) return true;
        else return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }

    void OnCollisionEnter(Collision col)
    {
        if(!CheckCollisionWithTarget) return;

        GameObject go = col.gameObject;
        Info nf = go.GetComponentInChildren<Info>();

        if(nf == null) return;

        if(DetectTypes.HasFlag(nf.DetectType))
        {
            if(!PriorityTypes.HasFlag(data.DetectType))
            {
                InColWithTargetMask = true;
                data = nf;
            }
        }
    }

    void OnCollisionExit(Collision col)
    {
        if(!CheckCollisionWithTarget) return;

        GameObject go = col.gameObject;
        Info nf = go.GetComponentInChildren<Info>();

        if(nf == null) return;

        if(DetectTypes.HasFlag(nf.DetectType))
        {
            InColWithTargetMask = false;
        }
    }
}
