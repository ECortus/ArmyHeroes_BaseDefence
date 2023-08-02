using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltProcessing : MonoBehaviour
{
    private UltProcessing _Inst => this;
    public static UltProcessing Instance { get; set; }
    void Awake() => Instance = _Inst;

    [SerializeField] private UltProcessing_IEnumerator DRONES, SHIELD, ORBHIT, GAUSS, MINES, ECONOMY;
    Coroutine dronesOn, shieldOn, orbhitOn, gaussOn, minesOn, economyOn;
    Coroutine dronesOff, shieldOff, orbhitOff, gaussOff, minesOff, economyOff;

#region TurnOn
    public void TurnOnDrones()
    {
        if(dronesOn == null)
        {
            if(dronesOff != null)
            {
                StopCoroutine(dronesOff);
                dronesOff = null;
            }

            dronesOn = StartCoroutine(DRONES.Process());
        }
    }

    public void TurnOnShield()
    {
        if(shieldOn == null)
        {
            if(shieldOff != null)
            {
                StopCoroutine(shieldOff);
                shieldOff = null;
            }

            shieldOn = StartCoroutine(SHIELD.Process());
        }
    }

    public void TurnOnMines()
    {
        if(minesOn == null)
        {
            if(minesOff != null)
            {
                StopCoroutine(minesOff);
                minesOff = null;
            }

            minesOn = StartCoroutine(MINES.Process());
        }
    }

    public void TurnOnOrbHit()
    {
        if(orbhitOn == null)
        {
            if(orbhitOff != null)
            {
                StopCoroutine(orbhitOff);
                orbhitOff = null;
            }

            orbhitOn = StartCoroutine(ORBHIT.Process());
        }
    }

    public void TurnOnGauss()
    {
        if(gaussOn == null)
        {
            if(gaussOff != null)
            {
                StopCoroutine(gaussOff);
                gaussOff = null;
            }

            gaussOn = StartCoroutine(GAUSS.Process());
        }
    }

    public void TurnOnEconomy()
    {
        if(economyOn == null)
        {
            if(economyOff != null)
            {
                StopCoroutine(economyOff);
                economyOff = null;
            }

            economyOn = StartCoroutine(ECONOMY.Process());
        }
    }
#endregion

#region TurnOff
    public void TurnOffDrones()
    {   
        if(dronesOff == null)
        {
            if(dronesOn != null)
            {
                StopCoroutine(dronesOn);
                dronesOn = null;
            }

            dronesOff = StartCoroutine(DRONES.Deprocess());
        }
    }

    public void TurnOffShield()
    {
        if(shieldOff == null)
        {
            if(shieldOn != null)
            {
                StopCoroutine(shieldOn);
                shieldOn = null;
            }

            shieldOff = StartCoroutine(SHIELD.Deprocess());
        }
    }

    public void TurnOffMines()
    {
        if(minesOff == null)
        {
            if(minesOn != null)
            {
                StopCoroutine(minesOn);
                minesOn = null;
            }

            minesOff = StartCoroutine(MINES.Deprocess());
        }
    }

    public void TurnOffOrbHit()
    {
        if(orbhitOff == null)
        {
            if(orbhitOn != null)
            {
                StopCoroutine(orbhitOn);
                orbhitOn = null;
            }

            orbhitOff = StartCoroutine(ORBHIT.Deprocess());
        }
    }

    public void TurnOffGauss()
    {
        if(gaussOff == null)
        {
            if(gaussOn != null)
            {
                StopCoroutine(gaussOn);
                gaussOn = null;
            }

            gaussOff = StartCoroutine(GAUSS.Deprocess());
        }
    }

    public void TurnOffEconomy()
    {
        if(economyOff == null)
        {
            if(economyOn != null)
            {
                StopCoroutine(economyOn);
                economyOn = null;
            }

            economyOff = StartCoroutine(ECONOMY.Deprocess());
        }
    }
#endregion
}
