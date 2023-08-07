using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Skins", fileName = "Skin00")]
public class SkinObject : ScriptableObject
{
    public int Index = 0;

    [Space]
    public int Cost = 100;

    [Space]
    public float HPBonus = 100f;
    public float DMGBonus = 100f;
}
