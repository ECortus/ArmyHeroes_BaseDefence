using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPointer : MonoBehaviour 
{
    public GameObject pointerPrefab;
    public bool HideOnDistance;

    private void Start() 
    {
        PointerManager.Instance.AddToList(this);
    }
    
    public void Delete() 
    {
        PointerManager.Instance.RemoveFromList(this);
        Destroy(this);
    }

}
