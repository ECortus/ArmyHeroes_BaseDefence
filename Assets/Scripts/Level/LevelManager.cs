using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; set; }

    [SerializeField] private List<Level> Levels = new List<Level>();

    private int _Index { get { return Statistics.LevelIndex; } set { Statistics.LevelIndex = value; } }
    public int GetIndex() => _Index % Levels.Count;
    public void SetIndex(int value)
    {
        if(value < 0) _Index = 0;
        else _Index = value;
    }

    public Level ActualLevel => Levels[GetIndex()];

    void Awake() => Instance = this;

    void Start()
    {
        LoadOnStart();
        /* StartLevel(); */
    }

    void LoadOnStart()
    {
        LoadLevel();
    }

    public void StartLevel()
    {
        ActualLevel.StartLevel();
    }

    public void EndLevel()
    {
        ActualLevel.EndLevel();

        int index = _Index;
        index += 1;
        SetIndex(index);
    }

    void LoadLevel()
    {
        /* BufferingLevel(); */
        Level level = ActualLevel;
        level.On();
    }

    public void NextLevel()
    {
        int index = _Index;
        index -= 1;
        SetIndex(index);

        OffLevel(ActualLevel);

        index = _Index;
        index += 1;
        SetIndex(index);

        LoadLevel();

        StartLevel();
    }

    public void PreviousLevel()
    {
        OffLevel(ActualLevel);

        int index = _Index;
        index -= 1;
        SetIndex(index);

        LoadLevel();

        StartLevel();
    }

    public void RestartLevel()
    {
        ActualLevel.ResetLevel();
        LandScene.Instance.On();
    }

    void OffLevel(Level level)
    {
        level.Off();
        /* level.Eliminate(); */
    }
}
