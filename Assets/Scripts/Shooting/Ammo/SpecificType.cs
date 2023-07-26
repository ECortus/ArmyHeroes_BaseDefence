using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
[Flags]
public enum SpecificType
{
    Simple = 1 << 0, 
    Poison = 1 << 1, 
    Fire = 1 << 2, 
    Electric = 1 << 3, 
    Freeze = 1 << 4, 
    Impulse = 1 << 5
}
