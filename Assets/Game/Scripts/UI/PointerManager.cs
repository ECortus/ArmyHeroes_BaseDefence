using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PointerManager : MonoBehaviour 
{
    public static PointerManager Instance { get; set; }

    private Dictionary<TargetPointer, PointerIcon> dictionary = new Dictionary<TargetPointer, PointerIcon>();
    [SerializeField] private Transform playerTransform;
    private Camera cam => Camera.main;

    [SerializeField] private RectTransform rectTransform;

    private void Awake() 
    {
        if (Instance == null) 
        {
            Instance = this;
        } 
        else 
        {
            Destroy(this);
        }
    }

    public void AddPointerToObject(GameObject go, GameObject prefab)
    {
        TargetPointer pointer = go.AddComponent<TargetPointer>();
        pointer.pointerPrefab = prefab;
    }

    public void AddToList(TargetPointer targetPointer) 
    {
        PointerIcon newPointer = Instantiate(targetPointer.pointerPrefab, transform).GetComponent<PointerIcon>();
        dictionary.Add(targetPointer, newPointer);
    }

    public void RemoveFromList(TargetPointer targetPointer) 
    {
        Destroy(dictionary[targetPointer].gameObject);
        dictionary.Remove(targetPointer);
    }

    public void RemoveAllPointers()
    {
        List<TargetPointer> list = dictionary.Keys.ToList();

        for(int i = 0; i < list.Count; i++)
        {
            TargetPointer tp = list[i];
            tp.Delete();
        }
    }

    void LateUpdate() 
    {
        // Left, Right, Down, Up
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);

        foreach (var kvp in dictionary) 
        {
            TargetPointer targetPointer = kvp.Key;
            PointerIcon pointerIcon = kvp.Value;

            if(!targetPointer.gameObject.activeInHierarchy)
            {
                pointerIcon.Hide();
                continue;
            }

            Vector3 toEnemy = targetPointer.transform.position - playerTransform.position;

            Ray ray = new Ray(playerTransform.position, toEnemy);
            /* Debug.DrawRay(playerTransform.position, toEnemy); */

            float rayMinDistance = Mathf.Infinity;

            for (int p = 0; p < 4; p++) 
            {
                if (planes[p].Raycast(ray, out float distance)) 
                {
                    if (distance < rayMinDistance) 
                    {
                        rayMinDistance = distance;
                    }
                }
            }

            rayMinDistance = Mathf.Clamp(rayMinDistance, 0, toEnemy.magnitude);

            if(targetPointer.HideOnDistance)
            {
                if (toEnemy.magnitude > rayMinDistance) 
                {
                    pointerIcon.Show();
                } 
                else 
                {
                    pointerIcon.Hide();
                    continue;
                }
            }
            else pointerIcon.Show();

            Vector3 worldPosition = ray.GetPoint(rayMinDistance);
            Vector3 position = cam.WorldToScreenPoint(worldPosition);
            position = TranslatePositionByResolution(position);

            Vector3 direction = (targetPointer.transform.position - playerTransform.position).normalized;
            var angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(-angle, Vector3.forward);

            pointerIcon.SetIconPosition(position, rotation);
        }
    }

    Vector3 TranslatePositionByResolution(Vector3 pos)
    {
        Vector3 position = pos;

        Vector2 size = new Vector2(
            rectTransform.rect.width * (Screen.width / 1080f),
            rectTransform.rect.height * (Screen.height / 1920f)
        );

        float limitX = (Screen.width - size.x) / 2;
        float limitY = (Screen.height - size.y) / 2;

        position = new Vector3(
            Mathf.Clamp(position.x, limitX, Screen.width - limitX),
            Mathf.Clamp(position.y, limitY, Screen.height - limitY),
            0f
        );

        return position;
    }
}
