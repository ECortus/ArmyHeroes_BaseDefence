using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltProcessing_IEnumerator : MonoBehaviour
{
    public virtual IEnumerator Process()
    {
        yield return null;
    }

    public virtual IEnumerator Deprocess()
    {
        yield return null;
    }
}
