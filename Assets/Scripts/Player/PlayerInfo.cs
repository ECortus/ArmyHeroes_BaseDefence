using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class PlayerInfo : Info
{
    public static PlayerInfo Instance { get; set; }
    void Awake() => Instance = this;

    [Header("Info: ")]
    [SerializeField] private float health;
    [SerializeField] private Weapon weapon;
    [SerializeField] private float DefaultMaxExp = 100f, ExpRequirePerProgress = 50f;

    public override float InputMaxHealth => health;
    public override float InputDamage => weapon.Damage;
    public float MaxExperience => DefaultMaxExp + Progress * ExpRequirePerProgress;

    void Start()
    {
        Heal(999f);
    }

    public override void Resurrect()
    {
        base.Resurrect();
        Heal(MaxHealth);
    }

    public override void Death()
    {
        base.Death();
    }

    public int Progress
    {
        get
        {
            return PlayerPrefs.GetInt(DataManager.ProgressKey, 0);
        }
        set
        {
            PlayerPrefs.SetInt(DataManager.ProgressKey, value);
            PlayerPrefs.Save();
        }
    }

    public async UniTask GetNewProgress()
    {
        await UniTask.Delay(0);
        Progress++;
    }

    public void ResetProgress()
    {
        Progress = 0;
    }
}
