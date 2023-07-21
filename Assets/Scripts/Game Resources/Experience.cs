using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public static class Experience
{
    private static float experience { get { return Statistics.Experience; } set { Statistics.Experience = value; } }

    public static void Plus(float count) 
    {
        experience += count;

        if(experience >= PlayerInfo.Instance.MaxExperience)
        {
            experience = 0f;
            PlayerInfo.Instance.GetNewProgress();
        }

        ExperienceUI.Instance.Refresh();
    }
    
    public static void Minus(float count)
    { 
        experience -= count;
        if(experience < 0) experience = 0;

        ExperienceUI.Instance.Refresh();
    }
}
