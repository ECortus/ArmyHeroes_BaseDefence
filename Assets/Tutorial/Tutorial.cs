using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Tutorial : MonoBehaviour
{
    public static Tutorial Instance { get; set; }

    [SerializeField] private HandShowHide MOVE;
    public bool MOVE_isDone;

    public bool Complete
    {
        get
        {
            return PlayerPrefs.GetInt(DataManager.TutorialKey, 0) == 1;
        }
        set
        {
            int val = value ? 1 : 0;
            PlayerPrefs.SetInt(DataManager.TutorialKey, val);
            PlayerPrefs.Save();
            /* Condition(); */
        }
    }

    [Space]
    public TutorialState State = TutorialState.NONE;

    void Awake()
    {   
        Instance = this;
    }

    void Start()
    {
        if(Complete)
        {
            MOVE_isDone = true;
            /* ACCELERATION_isDone = true; */
            return;
        }

        if(!Complete) 
        {

        }
        else
        {
            Off();
        }
    }

    void Update()
    {
        if(Complete) return;
    }

    public void SetState(TutorialState _state, bool done = false)
    {
        if(_state == TutorialState.NONE) OffAll();

        if(_state == State) return;

        OffAll();
        State = _state;

        switch(State)
        {
            case TutorialState.MOVE:
                MOVE.Open();
                break;
            default:
                Debug.Log("looser");
                break;
        }
    }

    public void Off()
    {
        Complete = true;
        MOVE_isDone = true;
        SetState(TutorialState.NONE);

        this.enabled = false;
    }

    void OffAll()
    {
        MOVE.Close();
    }

    public async void Condition()
    {
        if(Complete)
        {
            await UniTask.Delay(1500);
            gameObject.SetActive(false);
            Instance = null;
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}

public enum TutorialState
{
    NONE, MOVE
}
