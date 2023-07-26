using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : HP_DMG_SPD
{
    public static PlayerUpgrades Instance { get; set; }
    void Awake() => Instance = this;

    public ShootingUpgrades ShootingUpgrades;
}
