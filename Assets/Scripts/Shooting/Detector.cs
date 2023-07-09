using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Detector : MonoBehaviour
{
    [SerializeField] private float detectRange;
    [SerializeField] private LayerMask enemiesMask;

    [HideInInspector] public HumanoidController data;

    HumanoidController previous;
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
                SomeoneAround(detectRange, out data);
                previous = data;
            }

            if(data != null)
            {
                distanceToData = Vector3.Distance(data.transform.position, transform.position);

                if(data.Died || distanceToData > detectRange)
                {
                    Reset();
                    continue;
                }

                if(SomeoneAround(distanceToData, out data))
                {
                    if(data != previous)
                    {
                        Change();
                        previous = data;
                    }
                }

                Set();
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

    bool SomeoneAround(float dstnc, out HumanoidController data)
    {
        HumanoidController unit = null;

        List<Collider> cols = Physics.OverlapSphere(transform.position, dstnc, enemiesMask).ToList();
        if(cols.Count > 0)
        {
            cols = cols.OrderBy(x  => Vector3.Distance(transform.position, x.transform.position)).ToList();
            unit = cols[0].GetComponent<HumanoidController>();
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
}
