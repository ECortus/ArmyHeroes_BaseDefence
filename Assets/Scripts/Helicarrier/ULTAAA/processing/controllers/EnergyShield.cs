using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShield : MonoBehaviour
{
    [SerializeField] private SphereCollider sphere;
    private float force;

    public void On(float frc)
    {
        force = frc;
        gameObject.SetActive(true);
    }

    public void Off()
    {
        gameObject.SetActive(false);
    }

    void OnCollisionStay(Collision col)
    {
        GameObject go = col.gameObject;
        Enemy enemy = go.GetComponent<Enemy>();

        if(enemy != null)
        {
            enemy.ForceBack(force);
        }
    }
}
