using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunHandlerLobby : MonoBehaviour
{
    public static readonly int _Shooting = Animator.StringToHash("Shooting");

    [SerializeField] private GunHandler gunHandler;
    [SerializeField] private Animator Animator;

    void OnEnable()
    {
        UpdateAnimator();
        gunHandler.SetGunPair(PlayerPrefs.GetInt(DataManager.WeaponKey, 0));
    }

    void UpdateAnimator()
    {
        Animator.SetFloat(_Shooting, gunHandler.GunsCount);
    }
}
