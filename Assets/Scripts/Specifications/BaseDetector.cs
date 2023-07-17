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

    public bool CheckCollisionWithTarget = false;
    public bool InColWithTargetMask { get; set; }

    public bool AdditionalCondition(Detection dt) => AdditionalConditionToData(dt);
    protected virtual bool AdditionalConditionToData(Detection dt) => true;

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
        data = null;
    }

    protected virtual IEnumerator Working()
    {
        yield return null;
    }

    void OnCollisionEnter(Collision col)
    {
        if(!CheckCollisionWithTarget) return;

        GameObject go = col.gameObject;
        Detection nf = go.GetComponent<Detection>();

        if(nf == null) return;

        if(detectTypes.HasFlag(nf.Type))
        {
            if(!priorityTypes.HasFlag(nf.Type))
            {
                if(data != null)
                {
                    if(priorityTypes.HasFlag(data.Type))
                    {
                        return;
                    }
                }

                InColWithTargetMask = true;
                data = nf;
            }
        }
    }

    void OnCollisionExit(Collision col)
    {
        if(!CheckCollisionWithTarget) return;

        GameObject go = col.gameObject;
        Detection nf = go.GetComponentInChildren<Detection>();

        if(nf == null) return;
        InColWithTargetMask = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
