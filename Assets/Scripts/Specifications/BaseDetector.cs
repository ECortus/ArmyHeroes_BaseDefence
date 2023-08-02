using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDetector : MonoBehaviour, IAdditionalCondition
{
    [Header("DEBUG: ")]
    public Detection data;

    [Header("Par-s: ")]
    public Detection detection;
    public DetectType priorityTypes;
    public DetectType detectTypes;
    public float range;

    protected virtual void Reset() { }
    protected virtual void Change() { }
    protected virtual void Set() { }

    public bool CheckCollisionWithTarget = false;
    [HideInInspector] public bool InColWithTargetMask = false;

    public virtual bool AdditionalCondition(Detection dt) => true;

    Coroutine coroutine;

    public void On()
    {
        if(coroutine == null)
        {
            coroutine = StartCoroutine(Working());
        }
    }

    public void Stop()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    public void Off()
    {
        Stop();
        Reset();
    }

    protected virtual IEnumerator Working()
    {
        yield return null;
    }

    void OnCollisionEnter(Collision col)
    {
        if(!CheckCollisionWithTarget || InColWithTargetMask) return;

        GameObject go = col.gameObject;
        Detection nf = go.GetComponent<Detection>();

        if(nf == null)
        {
            InColWithTargetMask = false;
            return;
        }

        if(nf == data)
        {
            InColWithTargetMask = true;
        }
        else
        {
            if(data != null)
            {
                if(priorityTypes.HasFlag(data.Type))
                {
                    InColWithTargetMask = false;
                    return;
                }
            }

            if(detectTypes.HasFlag(nf.Type) && AdditionalCondition(nf))
            {
                InColWithTargetMask = true;
                data = nf;
                
                Change();
            }
        }
    }

    void OnCollisionExit(Collision col)
    {
        if(!CheckCollisionWithTarget) return;

        GameObject go = col.gameObject;
        Detection nf = go.GetComponent<Detection>();

        if(nf != null)
        {
            InColWithTargetMask = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
