using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiesController : MonoBehaviour
{
    [Range(0f, 100f)]
    [SerializeField] private float happyProbality, woundedProbality, callDocProbality, buildedProbality;
    [SerializeField] private ParticleSystem happy, wounded, callDoc, builded;

    public void PlayHappy()
    {
        float num = Random.Range(0f, 100f);
        if(num <= happyProbality) happy.Play();
    }
    
    public void PlayWounded()
    {
        float num = Random.Range(0f, 100f);
        if(num <= woundedProbality) wounded.Play();
    }
    
    public void PlayCallDoc()
    {
        float num = Random.Range(0f, 100f);
        if(num <= callDocProbality) callDoc.Play();
    }
    
    public void PlayBuilded()
    {
        float num = Random.Range(0f, 100f);
        if(num <= buildedProbality) builded.Play();
    }
}
