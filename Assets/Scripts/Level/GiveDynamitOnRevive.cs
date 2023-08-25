using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveDynamitOnRevive : MonoBehaviour
{
    [SerializeField] private DynamitBall dynamit;

    public void Put()
    {
        dynamit.On(Player.Instance.Transform.position);
    }
}
