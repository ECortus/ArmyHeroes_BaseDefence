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
    Enemy = 256, 
    Boss = 512,
    Crystal = 1024,
}
