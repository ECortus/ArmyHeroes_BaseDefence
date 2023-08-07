using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreListing : MonoBehaviour
{
    [SerializeField] private Transform listToMove;
    [SerializeField] private float topBound, bottomBound;
    [SerializeField] private float sensivity, listingSpeed;

    private float mouseStartY, diffMouseY, Y;
    private Vector3 lastListPos;

    void Start()
    {
        Y = listToMove.localPosition.y;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseStartY = Input.mousePosition.y;
            lastListPos = listToMove.localPosition;
            Y = lastListPos.y;
        }
        if (Input.GetMouseButton(0))
        {
            diffMouseY = (mouseStartY - Input.mousePosition.y) * sensivity;
            Y = lastListPos.y - diffMouseY;
            Y = Mathf.Clamp(Y, bottomBound, topBound);
        }

        Vector3 pos = new Vector3(0f, Y, 0f);
        listToMove.localPosition = Vector3.Lerp(listToMove.localPosition, pos, listingSpeed * Time.deltaTime);
    }
}
