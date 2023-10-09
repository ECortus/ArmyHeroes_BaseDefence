using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

public class StateMapOnWinLevelUI : MonoBehaviour
{
    [SerializeField] private Animator[] Animators;
    [SerializeField] private float lightTime = 2f, staticTime = 1f;

    Animator animator;
    int Index => Mathf.Clamp(Statistics.LevelIndex, 0, Animators.Length);

    public float time => lightTime + staticTime;

    public async void Refresh()
    {
        for(int i = 0; i < Animators.Length; i++)
        {
            animator = Animators[i];

            animator.enabled = false;
            animator.gameObject.SetActive(false);
        }

        for(int i = 0; i < Animators.Length; i++)
        {
            animator = Animators[i];

            if(Index > i)
            {
                animator.enabled = false;
                animator.gameObject.SetActive(true);
            }
            else if(Index < i)
            {
                break;
            }
            else
            {
                animator.gameObject.SetActive(true);
                animator.enabled = true;

                await UniTask.Delay((int)(lightTime * 1000));

                animator.enabled = false;
                Image image = animator.GetComponent<Image>();

                Color color = image.color;
                color.a = 1f;
                image.color = color;
            }

            await UniTask.Delay(50);
        }
    }
}
