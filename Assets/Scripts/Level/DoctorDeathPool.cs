using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorDeathPool : MonoBehaviour
{
    [SerializeField] private float resurrectTime = 30f;
    [SerializeField] private Transform[] spawnDots;

    private List<DoctorTimer> list = new List<DoctorTimer>();

    public class DoctorTimer
    {
        public Doctor Controller;
        public float Time = 0f;
    }

    public void AddDoctor(Doctor doc)
    {
        DoctorTimer doctor = new DoctorTimer
        {
            Controller = doc,
            Time = resurrectTime
        };

        list.Add(doctor);
    }

    void Update()
    {
        if(!GameManager.Instance.isActive) return;

        for(int i = 0; i < list.Count; i++)
        {
            list[i].Time -= Time.deltaTime;
            if(list[i].Time <= 0f)
            {
                Spawn(list[i].Controller);
                list.Remove(list[i]);
                i--;
            }
        }
    }

    void Spawn(Doctor doc)
    {   
        Vector3 pos = spawnDots[Random.Range(0, spawnDots.Length)].position;
        Quaternion rot = Quaternion.Euler(
            0f, Random.Range(0f, 360f), 0f
        );

        doc.On(pos, rot);
    }
}
