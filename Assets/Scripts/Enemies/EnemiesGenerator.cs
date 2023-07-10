using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class EnemiesGenerator : MonoBehaviour
{
    private float Radius = 30f;
    private Vector3 Center
    {
        get
        {
            return transform.position + new Vector3(0f, 1f, 0f);
        }
    }

    Coroutine coroutine;

    public void Launch()
    {
        if(coroutine == null) coroutine = StartCoroutine(Working());
    }

    public void Stop()
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

            break;
        }

        Stop();
    }

    void Spawn()
    {
        float randomX = Random.Range(0f, 1f);
        Vector3 pos = Center + new Vector3(randomX, 0f, Mathf.Pow((1 - Mathf.Pow(randomX, 2)), 1/2)) * Radius;
        Quaternion rot = Quaternion.Euler(0f, Vector3.Angle((pos - Center).normalized, transform.forward), 0f);

        EnemiesPool.Instance.Insert(EnemyType.Default, null, pos, rot);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(Center, Radius);
    }
}
