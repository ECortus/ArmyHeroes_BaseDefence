using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    private float speed = 80f;

    void Update()
    {
        transform.Rotate(new Vector3(0f, speed * Time.deltaTime, 0f), Space.World);
    }
}
