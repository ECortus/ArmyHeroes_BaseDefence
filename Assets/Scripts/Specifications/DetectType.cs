using System;

[Flags]
public enum DetectType
{
    Nothing = 0, 
    Player = 1, 
    Soldier = 2, 
    Engineer = 4, 
    Pikeman = 8, 
    Doctor = 16,
    Helicarrier = 32, 
    Wall = 64,
    Building = 128, 
    Fireposition = 256,
    Enemy = 512, 
    Boss = 1024,
    Crystal = 2048,
}
