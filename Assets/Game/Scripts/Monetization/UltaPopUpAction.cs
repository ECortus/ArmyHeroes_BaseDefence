using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltaPopUpAction : PopUpAction
{
    [SerializeField] private int Index = 75;

    public override void Do()
    {
        UltaActivator.Instance.ActivateByIndex(Index);
    }
}
