using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;

public class UpgradeButtonUI : MonoBehaviour
{
    [SerializeField] private Roulette roulette;
    [SerializeField] private UpgradeAction action;

    public void RefreshGetUI()
    {
        action.RefreshGetUI();
    }

    public void Apply()
    {
        action.Function();
        Refresh();
    }
    
    int Progress => action.Progress;

    [Space]
    [SerializeField] private Outline outline;

    public void OnOutline() => outline.enabled = true;
    public void OffOutline() => outline.enabled = false;

    [SerializeField] private GameObject equalZero, underZero;

    [Space]
    [SerializeField] private Image spriteImage;
    public Sprite Sprite => spriteImage.sprite;
    public TextMeshProUGUI textInfo;
    public Image getImage => roulette.choisedImage;
    public TextMeshProUGUI textGet => roulette.choisedText;

    public void Refresh()
    {
        if(Progress == 0)
        {
            underZero.SetActive(false);
            equalZero.SetActive(true);
        }
        else
        {
            underZero.SetActive(true);
            equalZero.SetActive(false);

            action.RefreshUI();
        }
    }
}
