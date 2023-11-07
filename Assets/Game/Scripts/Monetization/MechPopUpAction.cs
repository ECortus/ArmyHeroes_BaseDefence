using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechPopUpAction : PopUpAction
{
    [SerializeField] private HelicarrierInfo heli;
    
    public override void Do()
    {
        heli.StartMech();
    }
}
