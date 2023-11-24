using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create levels info list")]
public class LevelsListObject : ScriptableObject
{
    public LevelWavesInfo[] Infos;
}
