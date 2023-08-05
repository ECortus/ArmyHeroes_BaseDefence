using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeAction : MonoBehaviour
{
    UpgradeButtonUI _buttonUI;

    private UpgradeButtonUI buttonUI
    {
        get
        {
            if(_buttonUI == null)
            {
                _buttonUI = GetComponent<UpgradeButtonUI>();
            }

            return _buttonUI;
        }
    }

    public Sprite Sprite => buttonUI.Sprite;
    public Image infoImage => buttonUI.getImage;
    public TextMeshProUGUI textInfo  => buttonUI.textInfo;
    public TextMeshProUGUI textGet => buttonUI.textGet;

    public virtual int Progress { get; }

    public virtual void Function() { }
    public virtual void RefreshUI() { }
    public virtual void RefreshGetUI() { }
}
