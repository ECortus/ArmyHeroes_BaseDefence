using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinesProcessing : UltProcessing_IEnumerator
{
    [SerializeField] private UltMines info;
    [SerializeField] private List<Mine> MinesPool = new List<Mine>();

    public override IEnumerator Process()
    {
        int count = info.Count;
        Mine mine;

        for(int i = 0; i < MinesPool.Count; i++)
        {
            if(count == 0)
            {
                break;
            }

            mine = MinesPool[Random.Range(0, MinesPool.Count)];

            if(!mine.Active)
            {
                mine.On(info.Range);
                mine.SetDamage(info.Damage);

                count--;
            }
        }

        yield return null;
    }

    public override IEnumerator Deprocess()
    {
        yield return null;
    }
}
