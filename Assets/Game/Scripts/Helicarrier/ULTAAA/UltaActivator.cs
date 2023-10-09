using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltaActivator : MonoBehaviour
{
    public static UltaActivator Instance { get; set; }
    void Awake() => Instance = this;

    public List<Ulta> AllUltas;
    [SerializeField] private List<UltaActivatorUI> activatorUIs;

    public Ulta Ulta => AllUltas[Index];
    int Index = 0;

    void Start()
    {
        UpdateAllActivators();
    }

    public void UpdateAllActivators()
    {
        foreach(UltaActivatorUI act in activatorUIs)
        {
            act.Updating();
        }
    }

    public void ActivateByIndex(int index)
    {
        Index = index;
        UpdateAllActivators();
        
        activatorUIs[Index].OnButtonClickAd();
    }

    public void Activate()
    {
        turnLeft.interactable = false;
        turnRight.interactable = false;

        Ulta.Activate();
    }

    public void Deactivate()
    {
        turnLeft.interactable = true;
        turnRight.interactable = true;

        Ulta.Deactivate();
    }

    [Space]
    [SerializeField] private Button turnLeft;
    [SerializeField] private Button turnRight;

    public void TurnLeft()
    {
        Index--;
        Index = Index < 0 ? AllUltas.Count - 1 : Index;

        UpdateAllActivators();
    }

    public void TurnRight()
    {
        Index++;
        Index = Index > AllUltas.Count - 1 ? Index % AllUltas.Count : Index;

        UpdateAllActivators();
    }
}
