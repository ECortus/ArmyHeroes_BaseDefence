using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class PlayerExitFromHeli : MonoBehaviour
{
    [SerializeField] private Animator Animator;
    [SerializeField] private Transform playeranim;
    
    [Space]
    [SerializeField] private Transform oneExit;
    [SerializeField] private Transform twoExit;
    
    [Space]
    [SerializeField] private Transform oneEnter;
    [SerializeField] private Transform twoEnter;
    
    public async UniTask Exit()
    {
        await FromPointToPoint(oneExit, twoExit);
    }
    
    public async UniTask Enter()
    {
        await FromPointToPoint(oneEnter, twoEnter);
    }
    
    public async UniTask FromPointToPoint(Transform point1, Transform point2)
    {
        playeranim.gameObject.SetActive(true);
        
        playeranim.position = point1.position;
        Animator.enabled = true;
        Animator.SetFloat("Speed", 5f);
        
        playeranim.rotation = Quaternion.LookRotation(-(playeranim.position - point2.position).normalized);

        while (Vector3.Distance(playeranim.position, point2.position) > 0.1f)
        {
            playeranim.position = Vector3.MoveTowards(playeranim.position, point2.position, 3f * Time.unscaledDeltaTime);
            await UniTask.Delay((int)(Time.unscaledDeltaTime * 1000f), DelayType.UnscaledDeltaTime);
        }
        
        Animator.SetFloat("Speed", 0f);
        
        await UniTask.Delay(250);
        
        playeranim.gameObject.SetActive(false);
    }
}
