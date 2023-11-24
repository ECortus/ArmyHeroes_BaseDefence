using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; set; }

    [SerializeField] private Transform target;
    public void SetTarget(Transform trg)
    {
        target = trg;
    }
    public void ResetTarget()
    {
        target = Player.Instance.Transform;
    }

    public float speedMove, speedRotate = 2f;

    [SerializeField] private Vector3 defaultPosition, defaultRotation;

    private Vector3 position => target.position + defaultPosition;
    private Vector3 rotation => defaultRotation;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ResetTarget();

        /*defaultPosition = transform.position - target.position;
        defaultRotation = transform.eulerAngles;*/

        Reset();
    }

    public void Reset()
    {
        transform.position = position;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(
            transform.position, position, speedMove * Time.unscaledDeltaTime
        );

        transform.rotation = Quaternion.Slerp(
            transform.rotation, Quaternion.Euler(rotation), speedRotate * Time.unscaledDeltaTime
        );
    }
}
