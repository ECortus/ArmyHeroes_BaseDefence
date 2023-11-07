using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FirepositionsOperator : MonoBehaviour
{
    [Inject] public static FirepositionsOperator Instance { get; set; }
    [Inject] void Awake() => Instance = this;

    public List<FirepositionUser> Users = new List<FirepositionUser>();
    public List<Fireposition> Firepositions = new List<Fireposition>();

    public void PoolFireposition(Fireposition fp)
    {
        if(!Firepositions.Contains(fp))
        {
            Firepositions.Add(fp);
            
            foreach(FirepositionUser sr in Users)
            {
                if (sr != null)
                {
                    if(!sr.Called)
                    {
                        fp.CallUser(sr);
                        break;
                    }
                }
            }
        }
    }

    public void DepoolFireposition(Fireposition fp)
    {
        if(Firepositions.Contains(fp))
        {
            Firepositions.Remove(fp);
        }
    }

    public bool PoolUser(FirepositionUser user)
    {
        if (user == null) return false;
        
        if(!Users.Contains(user))
        {
            Users.Add(user);
            
            foreach(Fireposition fp in Firepositions)
            {
                if(!fp.Busy && !fp.CallSomeone)
                {
                    fp.CallUser(user);
                    return true;
                }
            }
        }

        return false;
    }

    public void DepoolUser(FirepositionUser user)
    {
        if(Users.Contains(user))
        {
            Users.Remove(user);
        }
    }

    public void Call()
    {
        FirepositionUser user = null;

        foreach(FirepositionUser sr in Users)
        {
            if(!sr.Called)
            {
                user = sr;
                break;
            }
        }

        if(user != null)
        {
            foreach(Fireposition fp in Firepositions)
            {
                if(!fp.Busy && !fp.CallSomeone)
                {
                    fp.CallUser(user);
                    break;
                }
            }
        }
    }
}
