using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorDetector : MonoBehaviour
{
    [Space]
    public Doctor controller;
    [SerializeField] private float HealRange = 2f;

    private List<Info> dataList => DetectorPool.Instance.Allies;
    private Info data;

    private Transform target => controller.target;
    private Coroutine coroutine;

    public bool Healing => coroutine != null;

    [SerializeField] private Transform HealPoint;

    void Update()
    {
        if(data == null)
        {
            foreach(var dt in dataList)
            {
                if(data != null && data.Died && data.Active)
                {
                    data = dt;
                    break;
                }
            }   
            return;
        }

        if(data != null)
        {
            controller.takeControl = false;
            StartHeal();
        }
        else
        {
            controller.takeControl = true;
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

            data = null;
        }
    }

    IEnumerator Heal()
    {
        HumanoidController patient = data.GetComponentInParent<HumanoidController>();

        controller.SetTarget(data.transform);
        yield return new WaitUntil(() => Vector3.Distance(transform.position, data.transform.position) < HealRange);
        patient.Off();
        
        controller.SetTarget(HealPoint);
        yield return new WaitUntil(() => Vector3.Distance(transform.position, HealPoint.position) < HealRange);

        controller.Off();
        yield return new WaitForSeconds(2f);

        controller.On(HealPoint.position);

        data.Resurrect();
        patient.On(HealPoint.position);

        controller.ResetTarget();
        StopHeal();

        yield return null;
    }
}
