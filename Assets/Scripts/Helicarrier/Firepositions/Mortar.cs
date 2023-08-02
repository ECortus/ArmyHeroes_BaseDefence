using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar : Fireposition
{
    [Space]
    [SerializeField] private List<GameObject> models = new List<GameObject>();
    [SerializeField] private List<GameObject> chelickObjs = new List<GameObject>();
    [SerializeField] private GunHandler gunHandler;
    [SerializeField] private MortarDetector detector;

    protected override void EnableAction()
    {
        chelickObjs[upgrader.Progress].SetActive(true);
        gunHandler.SetGunPair(upgrader.Progress);

        detector.On();
    }

    protected override void DisableAction()
    {
        detector.Off();

        chelickObjs[upgrader.Progress].SetActive(false);
        gunHandler.SetGunPair(upgrader.Progress);
    }

    int prevT = -1;
    public override void RefreshModel()
    {
        int t = -1;

        if(HP <= 0f) t = upgrader.Progress * 2;
        else t = upgrader.Progress * 2 + 1;

        t = Mathf.Clamp(t, 0, models.Count - 1);

        if(prevT != t)
        {
            for(int i = 0; i < models.Count; i++)
            {
                if(i == t)
                {
                    chelickObjs[i / 2].SetActive(HP > 0f && Busy);
                    
                    models[i].SetActive(true);

                    if(HP > 0f && Busy) gunHandler.SetGunPair(i / 2);
                    else gunHandler.Disable();
                }
                else
                {
                    chelickObjs[i / 2].SetActive(false);

                    gunHandler.Disable();
                    models[i].SetActive(false);
                }
            }
        }

        prevT = t;
    }
}
