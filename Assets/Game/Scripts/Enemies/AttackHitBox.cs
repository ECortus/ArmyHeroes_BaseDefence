using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    [SerializeField] private string[] Tags;
    [SerializeField] private float damage;

    private BoxCollider col;

    private void Start()
    {
        if (col == null)
        {
            col = GetComponent<BoxCollider>();
        }

        Off();
    }

    public void On()
    {
        /*gameObject.SetActive(true);*/
        col.enabled = true;
    }

    public void Off()
    {
        /*gameObject.SetActive(false);*/
        col.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        Detection det;

        if (Tags.Contains(go.tag))
        {
            if (TryGetComponent<Detection>(out det))
            {
                det.GetHit(damage);
            }
        }
    }
}
