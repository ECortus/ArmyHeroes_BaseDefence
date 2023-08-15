using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class StatesMap : MonoBehaviour
{
    [SerializeField] private Animator[] Animators;

    [SerializeField] private bool RefreshByStep = false;
    [SerializeField] private bool AnimationOn = true;

    Animator animator;
    int Index => Mathf.Clamp(Statistics.LevelIndex, 0, Animators.Length);

    void OnEnable()
    {
        if(RefreshByStep)
        {
            RefreshSteply();
        }
        else
        {
            Refresh();
        }
    }

    public async void RefreshSteply()
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
                
                if(AnimationOn)
                {
                    animator.enabled = true;
                }
                else
                {
                    animator.enabled = false;
                }
            }

            await UniTask.Delay(50);
        }
    }

    public void Refresh()
    {
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
                animator.enabled = false;
                animator.gameObject.SetActive(false);
            }
            else
            {
                animator.gameObject.SetActive(true);
                
                if(AnimationOn)
                {
                    animator.enabled = true;
                }
                else
                {
                    animator.enabled = false;
                }
            }
        }
    }
}
