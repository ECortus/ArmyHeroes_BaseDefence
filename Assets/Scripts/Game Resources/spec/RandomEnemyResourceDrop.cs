using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RandomEnemyResourceDrop : MonoBehaviour
{
    [System.Serializable]
    public class ProbalityDrop
    {
        [Range(0, 100)]
        public float Probality;
        public ResourceDrop Resource;
    }

    [SerializeField] private List<ProbalityDrop> dropProbality = new List<ProbalityDrop>();

    public void Drop(int count)
    {
        int t = 0;
        for (int i = 0; i < count; i++)
        {
            t = RandomIndex();

            if(t == 0) dropProbality[t].Resource.ResourcePerBallMod = KillingRewardLVLs.EnemyKillingEXPRewardMod;

            dropProbality[t].Resource.Drop();
        }
    }

    int RandomIndex()
    {
        List<float> probalities = new List<float>();
        float whole = 0f;

        foreach(ProbalityDrop prob in dropProbality)
        {
            whole += prob.Probality;
            probalities.Add(whole);
        }

        float random = Random.Range(0f, whole);
        int index = 0;

        for(int i = 0; i < probalities.Count; i++)
        {   
            if(random <= probalities[i])
            {
                index = i;
                break;
            }
        }

        return index;
    }
}
