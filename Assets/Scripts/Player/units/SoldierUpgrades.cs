using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierUpgrades : HP_DMG_SPD
{
    [SerializeField] private UpgradeSoldiers upgradeSoldiers;

    void Start()
    {
        PlayerNewProgress.Instance.RefreshBonus(upgradeSoldiers);
    }
}