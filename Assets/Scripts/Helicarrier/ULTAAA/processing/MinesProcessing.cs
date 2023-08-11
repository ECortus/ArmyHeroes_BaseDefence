using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinesProcessing : UltProcessing_IEnumerator
{
    [SerializeField] private UltMines info;
    [SerializeField] private Mine MinePref;
    private List<Mine> MinesPool = new List<Mine>();

    [Space]
    [SerializeField] private Transform toRotate;
    [SerializeField] private Transform muzzle;

    [Space]
    [SerializeField] private Animation anim;

    public Mine Insert(Mine obj)
    {
        Mine Mine = null;
        List<Mine> list = MinesPool;

        if(list.Count > 0)
        {
            foreach(Mine mn in list)
            {
                if(mn == null) continue;

                if(!mn.Active)
                {
                    Mine = mn;
                    break;
                }
            }
        }
        
        if(Mine == null)
        {
            Mine = Instantiate(obj);
            MinesPool.Add(Mine);
        }

        return Mine;
    }

    public void ResetPool()
    {
        MinesPool.Clear();
    }

    Vector2 dir2;
    Vector3 dir;

    float angle;

    public override IEnumerator Process()
    {
        angle = Random.Range(0f, 360f);
        toRotate.eulerAngles = new Vector3(0f, angle, 0f);

        anim.Play("bounceSpawn");
        yield return new WaitForSeconds(anim.GetClip("bounceSpawn").length);

        int count = info.Count;
        Mine mine;

        for(int i = 0; i < count; i++)
        {
            mine = Insert(MinePref);
            mine.On(info.Range, muzzle.position);
            mine.SetDamage(info.Damage);

            dir2 = Random.insideUnitCircle.normalized;
            dir2.y = Mathf.Abs(dir2.y);

            dir = new Vector3(dir2.x, 0.65f, dir2.y);
            dir = muzzle.TransformDirection(dir);

            mine.Force(dir, Random.Range(400, 500f) * 1.65f);
        }

        yield return new WaitForSeconds(2f);

        anim.Play("bounceSpawnReverse");
    }

    public override IEnumerator Deprocess()
    {
        yield return null;
    }
}
