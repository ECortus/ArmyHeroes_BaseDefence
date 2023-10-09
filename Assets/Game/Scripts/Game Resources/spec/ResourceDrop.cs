using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDrop : MonoBehaviour
{
    [SerializeField] private ResourceType type;
    public int BallAmount;
    public float ResourcePerBall;

    public float ResourcePerBallMod { get; set; }

    public void Drop()
    {
        DropAmount((int)BallAmount);
    }

    public void DropAmount(int count)
    {
        ResourceBall ball = null;
        Vector3 dir = Vector3.zero;

        for(int i = 0; i < count; i++)
        {
            dir = Random.insideUnitSphere;
            dir.y = 1f;

            ball = ResourcePool.Instance.Insert(type, transform.position + Vector3.up * 1.25f);
            ball.SetRecource(ResourcePerBall * (1f + ResourcePerBallMod));
            ball.Force(dir, 300f * Random.Range(0.35f, 1f));
        }
    }
}
