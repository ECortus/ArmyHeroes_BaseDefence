using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDrop : MonoBehaviour
{
    [SerializeField] private ResourceType type;
    [SerializeField] private float ballAmount, resourcePerBall;

    public void Drop()
    {
        ResourceBall ball = null;
        Vector3 dir = Vector3.zero;

        for(int i = 0; i < ballAmount; i++)
        {
            dir = Random.insideUnitSphere;
            dir.y = 0.7f;

            ball = ResourcePool.Instance.Insert(type, transform.position);
            ball.SetRecource(resourcePerBall);
            ball.Force(dir, 400f * Random.Range(0f, 1f));
        }
    }
}
