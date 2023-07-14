using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorDetector : MonoBehaviour
{
    [Space]
    public Doctor controller;
    [SerializeField] private float HealRange = 2f, HealDelay = 2f;

    private Info data;

    private Transform target => controller.target;
    private Coroutine coroutine;

    public bool Healing => coroutine != null;

    void Update()
    {
        if(data == null) return;
        else
        {
            if(data.MaxHealth == data.Health)
            {
                controller.takeControl = true;
                return;
            }
            else
            {
                controller.takeControl = false;
            }
        }

        if(Vector3.Distance(transform.position, target.position) <= HealRange)
        {
            controller.takeControl = true;
            StartHeal();
        }
        else
        {
            controller.takeControl = false;
            StopHeal();
        }
    }

    void StartHeal()
    {
        if(coroutine == null) coroutine = StartCoroutine(Heal());
    }

    void StopHeal()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    IEnumerator Heal()
    {
        while(true)
        {
            if(data == null || data.Died || !data.Active)
            {
                StopHeal();
                break;
            }

            /* if(data.Health == data.MaxHealth)
            {
                controller.takeControl = false;
                yield return new WaitUntil(() => 
                    data.Health != data.MaxHealth);

                yield return new WaitUntil(() => 
                    InColWithTargetMask || Vector3.Distance(transform.position, target.position) <= HealRange);
                controller.takeControl = true;
            } */

            controller.Heal(data);
            yield return new WaitForSeconds(HealDelay);
        }
    }
}
