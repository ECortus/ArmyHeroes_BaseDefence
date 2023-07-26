using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirepositionUser : MonoBehaviour
{
    public Detection User;
    public bool isUsed => User != null;
    public void PutUser(Detection us)
    {
        User = us;
    }
}
