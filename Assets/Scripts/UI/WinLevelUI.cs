using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class WinLevelUI : OpenCloseObjectLevelUI
{
    [SerializeField] private EndLevelStatsUI statsUI;
    [SerializeField] private StateMapOnWinLevelUI map;

    [Space]
    [SerializeField] private RectTransform toMove;
    [SerializeField] private float speed = 5f, moveToY = 1750f;

    float Y = 0f;

    public override void Open()
    {
        GameManager.Instance.SetActive(false);

        base.Open();
        statsUI.Refresh();

        StartCoroutine(Listing());
    }

    IEnumerator Listing()
    {
        Y = 0f;
        toMove.anchoredPosition = Vector2.zero;

        map.Refresh();

        yield return new WaitForSeconds(map.time);

        while(Y < moveToY)
        {
            Y += Time.unscaledDeltaTime * speed;
            Y = Mathf.Clamp(Y, 0f, moveToY);
            toMove.anchoredPosition = new Vector2(0f, Y);

            yield return null;
        }
    }
}
