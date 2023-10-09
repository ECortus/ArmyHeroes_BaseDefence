using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronesProcessing : UltProcessing_IEnumerator
{
    [SerializeField] private UltDrones info;
    [SerializeField] private List<Drone> DronesPool = new List<Drone>();

    private List<Drone> ActiveDrones = new List<Drone>();

    public override IEnumerator Process()
    {
        int count = info.Count;
        Drone drone;

        for(int i = 0; i < DronesPool.Count; i++)
        {
            if(count == 0)
            {
                break;
            }

            drone = DronesPool[i];

            if(!drone.Active)
            {
                var vector2 = Random.insideUnitCircle.normalized * 3f;

                drone.On(new Vector3(vector2.x, 0, vector2.y));
                count--;

                ActiveDrones.Add(drone);
            }
        }

        yield return null;
    }

    public override IEnumerator Deprocess()
    {
        foreach(Drone drone in ActiveDrones)
        {
            drone.Off();
        }

        ActiveDrones.Clear();

        yield return null;
    }
}
