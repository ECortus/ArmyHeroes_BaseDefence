using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPUAnimator : MonoBehaviour
{
    [SerializeField] private Animation Animation;

    public void SetAnimation(string name)
    {
        Animation.Stop();
        Animation.Play(name);
    }
}
