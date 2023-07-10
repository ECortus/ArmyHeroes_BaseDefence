using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBall : MonoBehaviour
{
    public float resourceAmount { get; set; }
    [SerializeField] private float distanceToPlayer, speed;

    [Space]
    [SerializeField] private Rigidbody rb;

    bool move = false;
    Transform target => Player.Instance.Transform;

    public bool Active => gameObject.activeSelf;

    public void On(Vector3 pos = new Vector3())
    {
        if(pos != new Vector3()) transform.position = pos;

        move = false;
        gameObject.SetActive(true);
    }

    public void Off()
    {
        gameObject.SetActive(false);
    }

    public void SetRecource(float amount)
    {
        resourceAmount = amount;
    }

    public void Force(Vector3 direction, float force)
    {
        rb.AddForce(direction * force);
    }

    public virtual void AddRecourceToPlayer()
    {
        Off();
    }

    void Update()
    {
        if(move)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, speed * Time.deltaTime);
            return;
        }

        if(Vector3.Distance(transform.position, target.position) < distanceToPlayer)
        {
            move = true;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        GameObject go = col.gameObject;

        switch(go.tag)
        {
            case "Player":
                AddRecourceToPlayer();
                break;
            default:
                break;
        }
    }
}
