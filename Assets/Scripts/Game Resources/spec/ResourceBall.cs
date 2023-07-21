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

    float spawnDelay = 0.5f;
    float time = 0f;

    public void On(Vector3 pos = new Vector3())
    {
        if(pos != new Vector3()) transform.position = pos;

        SetMove(false);
        gameObject.SetActive(true);

        time = spawnDelay;
    }

    public void Off()
    {
        gameObject.SetActive(false);
    }

    public void SetRecource(float amount)
    {
        resourceAmount = amount;
    }

    public void SetMove(bool val)
    {
        move = val;
    }

    public void Force(Vector3 direction, float force)
    {
        rb.AddForce(direction * force);
    }

    protected virtual void AddRecourceToPlayer()
    {
        Off();
    }

    void Update()
    {
        if(time > 0f)
        {
            time -= Time.deltaTime;
            return;
        }

        if(move)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, speed * Time.deltaTime);
            return;
        }

        if(Vector3.Distance(transform.position, target.position) < distanceToPlayer)
        {
            SetMove(true);
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
