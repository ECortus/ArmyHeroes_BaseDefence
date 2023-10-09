using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTip : MonoBehaviour
{
    [SerializeField] private GameObject main;
    [SerializeField] private GameObject hand;

    [Space] [SerializeField] private float delay = 5f;
    [SerializeField] private int lvl = 1;

    private float time = 0f;

    void OnHand()
    {
        if (!hand.activeSelf)
        {
            hand.SetActive(true);
        }
    }

    void OffHand()
    {
        if (hand.activeSelf)
        {
            hand.SetActive(false);
        }
    }

    void CheckLVL()
    {
        if (Statistics.LevelIndex > lvl)
        {
            Destroy(this);
        }
    }
    
    void Start()
    {
        CheckLVL();
    }

    void Update()
    {
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0) || !main.activeSelf)
        {
            time = 0f;
            OffHand();
            return;
        }

        time += Time.deltaTime;

        if (time >= delay)
        {
            OnHand();
        }
    }
}
