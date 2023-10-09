using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinChoiser : MonoBehaviour
{
    [HideInInspector] public SkinButton ShowedButton = null;

    [Space]
    [SerializeField] private SkinButton[] Buttons;

    [Space]
    [SerializeField] private Transform previewParent;

    public void SetPreview(GameObject preview)
    {
        if(previewParent.childCount > 0)
        {
            foreach(Transform item in previewParent)
            {
                Destroy(item.gameObject);
            }
        }

        GameObject go = Instantiate(preview, previewParent);

        go.transform.localPosition = Vector3.zero;
        /* go.transform.localEulerAngles = new Vector3(0f, 180f, 0f); */
        go.transform.localScale = Vector3.one;
    }

    [Space]
    [SerializeField] private Image image;
    [SerializeField] private Sprite saled, buyed;
    
    [Space]
    [SerializeField] private TextMeshProUGUI stateText;
    [SerializeField] private TextMeshProUGUI costText;

    [Space]
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI dmgText;

    void OnEnable()
    {
        if(ShowedButton == null)
        {
            foreach (var item in Buttons)
            {
                if(item.skin.Index == PlayerSkin.Index)
                {
                    ShowedButton = item;
                }
            }
        }

        SetShowSkin(ShowedButton);
    }

    public void Refresh()
    {

    }

    public void OnBuyButtonClick()
    {
        if(Statistics.Token >= ShowedButton.skin.Cost && !ShowedButton.Buyed)
        {
            ShowedButton.Buy();
        }
    }

    public void SetShowSkin(SkinButton button)
    {
        SkinObject skin = button.skin;

        if(button.Buyed)
        {
            costText.gameObject.SetActive(false);
            stateText.gameObject.SetActive(true);

            stateText.text = "Equipped";

            image.sprite = buyed;
        }
        else
        {
            costText.gameObject.SetActive(true);
            stateText.gameObject.SetActive(false);

            costText.text = $"{skin.Cost}";

            image.sprite = saled;
        }

        ShowedButton = button;

        SetPreview(button.skinGameObject);

        hpText.text = $"+{skin.HPBonus}%";
        dmgText.text = $"+{skin.DMGBonus}%";

        RefreshButtons();
    }

    public void RefreshButtons()
    {
        foreach(SkinButton item in Buttons)
        {
            item.Refresh();
        }
    }
}
