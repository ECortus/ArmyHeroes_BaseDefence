using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialArrow : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform toRotate;

    [SerializeField] private Transform pinTo;

    private Transform target;
    private Vector3 angles;
    private Quaternion rotation;
    
    public void SetTarget(Transform trg)
    {
        target = trg;
    }

    public void On()
    {
        gameObject.SetActive(true);
    }

    public void Off()
    {
        target = null;
        gameObject.SetActive(false);
    }
    
    void Update()
    {
        transform.position = pinTo.position;
        
        if (target != null)
        {
            rotation = Quaternion.LookRotation((target.position - transform.position).normalized);
            
            angles = rotation.eulerAngles;
            angles.x = 0f;
            angles.z = 0;
            
            toRotate.rotation = Quaternion.Slerp(toRotate.rotation, Quaternion.Euler(angles), speed * Time.deltaTime);
        }
    }
}
