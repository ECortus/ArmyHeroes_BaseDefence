using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltProcessing : MonoBehaviour
{
    private UltProcessing _Inst => this;
    public static UltProcessing Instance { get; set; }
    void Awake() => Instance = _Inst;

    [SerializeField] private UltProcessing_IEnumerator DRONES, SHIELD, ORBHIT, GAUSS, MINES, ECONOMY;
    Coroutine drones, shield, orbhit, gauss, mines, economy;

#region TurnOn
    public void TurnOnDrones()
    {
        if(drones == null)
        {
            drones = StartCoroutine(DRONES.Process());
        }
    }

    public void TurnOnShield()
    {
        if(shield == null)
        {
            shield = StartCoroutine(SHIELD.Process());
        }
    }

    public void TurnOnMines()
    {
        if(mines == null)
        {
            mines = StartCoroutine(MINES.Process());
        }
    }

    public void TurnOnOrbHit()
    {
        if(orbhit == null)
        {
            orbhit = StartCoroutine(ORBHIT.Process());
        }
    }

    public void TurnOnGauss()
    {
        if(gauss == null)
        {
            gauss = StartCoroutine(GAUSS.Process());
        }
    }

    public void TurnOnEconomy()
    {
        if(economy == null)
        {
            economy = StartCoroutine(ECONOMY.Process());
        }
    }
#endregion

#region TurnOff
    public void TurnOffDrones()
    {   
        if(drones != null)
        {
            StopCoroutine(drones);
            drones = null;
        }
    }

    public void TurnOffShield()
    {
        if(shield != null)
        {
            StopCoroutine(shield);
            shield = null;
        }
    }

    public void TurnOffMines()
    {
        if(mines != null)
        {
            StopCoroutine(mines);
            mines = null;
        }
    }

    public void TurnOffOrbHit()
    {
        if(orbhit != null)
        {
            StopCoroutine(orbhit);
            orbhit = null;
        }
    }

    public void TurnOffGauss()
    {
        if(gauss != null)
        {
            StopCoroutine(gauss);
            gauss = null;
        }
    }

    public void TurnOffEconomy()
    {
        if(economy != null)
        {
            StopCoroutine(economy);
            economy = null;
        }
    }
#endregion
}
