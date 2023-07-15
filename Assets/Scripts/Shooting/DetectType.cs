using System;

[Flags]
public enum DetectType
{
    Nothing = 0, 
    Player = 1, 
    Ally = 2, 
    Helicarrier = 4, 
    Building = 8, 
    Enemy = 16, 
    Crystal = 32,
}
