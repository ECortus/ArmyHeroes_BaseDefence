using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class PlayerInfo : Detection
{
    public static PlayerInfo Instance { get; set; }
    void Awake() => Instance = this;

    [Header("Player info: ")]
    [SerializeField] private Weapon weapon;
    [SerializeField] private float DefaultMaxExp = 100f, ExpRequirePerProgress = 50f;

    public float Damage => weapon.Damage;
    public float MaxExperience => DefaultMaxExp + Progress * ExpRequirePerProgress;

    void Start()
    {
        Heal(999f);
    }

    public void Hit(Detection dtct)
    {
        if(dtct != null) dtct.GetHit(Damage);
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

        Debug.Log("new level");
    }

    public void ResetProgress()
    {
        Progress = 0;
    }
}
