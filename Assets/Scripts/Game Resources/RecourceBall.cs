using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecourceBall : MonoBehaviour
{
    public float recourceAmount { get; set; }
    [SerializeField] private float distanceToPlayer, speed;

    [Space]
    [SerializeField] private Rigidbody rb;

    bool move = false;
    Transform target => Player.Instance.Transform;
    float defaultY = 0.75f;

    public void On()
    {
        move = false;
        gameObject.SetActive(true);
    }

    public void Off()
    {
        gameObject.SetActive(false);
    }

    public void SetRecource(float amount)
    {
        recourceAmount = amount;
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

        transform.position = new Vector3(
            transform.position.x, 
            transform.position.y < defaultY ? defaultY : transform.position.y, 
            transform.position.z
        );

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
