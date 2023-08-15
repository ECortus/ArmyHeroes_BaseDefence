using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicarrierExit : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        GameObject go = col.gameObject;

        if(go.tag == "Player")
        {
            UI.Instance.EndLevel();
        }
    }
}
