using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    void Awake() => Instance = this;

    public GameObject Insert(ObjectType type, GameObject obj, Vector3 pos = new Vector3(), Vector3 direction = new Vector3(), Vector3 rot = new Vector3())
    {
        switch(type)
        {
            default:
                break;
        }

        return null;
    }

    public void Add(ObjectType type, GameObject obj)
    {
        switch(type)
        {
            default:
                break;
        }
    }

    public void Delete(ObjectType type, GameObject obj)
    {
        switch(type)
        {
            default:
                break;
        }
    }
}

[System.Serializable]
public enum ObjectType
{
    Default
}
