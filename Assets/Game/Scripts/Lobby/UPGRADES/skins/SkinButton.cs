using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinButton : MonoBehaviour
{
    public bool DefaultBuyedAndEquipped = false;

    public bool Buyed
    {
        get
        {
            return PlayerPrefs.GetInt(DataManager.SkinKey + "_Buyed_state_" + skin.Index, 0) > 0 
                || DefaultBuyedAndEquipped;
        } 
        set
        {
            PlayerPrefs.SetInt(DataManager.SkinKey + "_Buyed_state_" + skin.Index, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public bool Choised => skin.Index == PlayerSkin.Index;

    [SerializeField] private SkinChoiser choiser;

    [Space]
    public SkinObject skin;

    [Space]
    public GameObject skinGameObject;

    [Space]
    [SerializeField] private Image image;
    [SerializeField] private Sprite saled, unchoised, choised;

    [Space]
    [SerializeField] private TextMeshProUGUI stateText;
    [SerializeField] private TextMeshProUGUI costText;
    
    public void OnButtonClick()
    {
        if(Buyed)
        {
            PlayerSkin.SetSkin(skin);
        }

        choiser.SetShowSkin(this);
    }

    public void Buy()
    {
        Buyed = true;
        Token.Minus(skin.Cost);

        OnButtonClick();
    }

    public void Refresh()
    {
        if(Buyed)
        {
            costText.gameObject.SetActive(false);
            stateText.gameObject.SetActive(true);

            if(Choised)
            {
                image.sprite = choised;
                stateText.text = "In use";
            }
            else
            {
                image.sprite = unchoised;
                stateText.text = "Available";
            }
        }
        else
        {
            costText.gameObject.SetActive(true);
            stateText.gameObject.SetActive(false);

            image.sprite = saled;
            costText.text = $"{skin.Cost}";
        }
    }
}
