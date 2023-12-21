using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToggler : MonoBehaviour
{
    [SerializeField] private GameObject main, skills, playerSkills, heliSkills, skins;

    void Start()
    {
        OpenMainMenu();
    }
    
    void OffAll()
    {
        main.SetActive(false);
        skills.SetActive(false);
        playerSkills.SetActive(false);
        heliSkills.SetActive(false);
        skins.SetActive(false);
    }
    
    public void OpenMainMenu()
    {
        OffAll();
        main.SetActive(true);
        
        InterstationalTimer.Instance.TryShowInterstationalAd();
    }
    
    public void OpenSkins()
    {
        OffAll();
        skins.SetActive(true);
        
        InterstationalTimer.Instance.TryShowInterstationalAd();
    }
    
    public void OpenPlayerSkills()
    {
        OffAll();
        skills.SetActive(true);
        playerSkills.SetActive(true);
        heliSkills.SetActive(false);
        
        InterstationalTimer.Instance.TryShowInterstationalAd();
    }
    
    public void OpenHeliSkills()
    {
        OffAll();
        skills.SetActive(true);
        playerSkills.SetActive(false);
        heliSkills.SetActive(true);
        
        InterstationalTimer.Instance.TryShowInterstationalAd();
    }
}
