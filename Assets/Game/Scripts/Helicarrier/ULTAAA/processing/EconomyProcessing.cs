using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class EconomyProcessing : UltProcessing_IEnumerator
{
    [SerializeField] private UltEconomy info;
    [SerializeField] private Animation anim;

    [Space]
    [SerializeField] private Transform drop;

    [Space]
    [SerializeField] private Animation[] boxes;

    float requireGold = 0;

    public override IEnumerator Process()
    {
        anim.Play("bounceSpawn");

        requireGold = info.GoldPerCrystal * Statistics.Crystal;
        Crystal.Minus(Statistics.Crystal);

        float goldPerGold = requireGold / info.Duration;

        while(requireGold > 0f)
        {
            for(int i = 0; i < boxes.Length; i++)
            {
                PlayBox(i, goldPerGold);
                yield return new WaitForSeconds(boxes[i].GetClip("resOnConvAnim").length / 3f);
            }
        }

        yield return null;
    }

    async void PlayBox(int ind, float goldPerGold)
    {
        boxes[ind].Play();

        await UniTask.Delay((int)(boxes[ind].GetClip("resOnConvAnim").length * 1000f));

        DropThreeGold(goldPerGold);
        requireGold -= goldPerGold;
    }

    Vector2 dir2;
    Vector3 dir;
    ResourceBall ball;

    public void DropThreeGold(float rec)
    {
        float res = rec / 3;

        for(int i = 0; i < 3; i++)
        {
            ball = ResourcePool.Instance.Insert(ResourceType.Gold, drop.position);
            ball?.SetRecource(res);

            dir2 = Random.insideUnitCircle.normalized;
            dir2.y = Mathf.Abs(dir2.y);

            dir = new Vector3(dir2.x, 1f, dir2.y);
            dir = drop.TransformDirection(dir);

            ball?.Force(dir, 300f * Random.Range(0.35f, 1f));
        }
    }

    public override IEnumerator Deprocess()
    {
        anim.Play("bounceSpawnReverse");
        yield return null;
    }
}
