using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public Camera mainCamera;
    Plane plane = new Plane(Vector3.up, 0);
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float distance;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance)){
            Vector3 worldPosition = ray.GetPoint(distance);
            transform.position = worldPosition;
        }
    }
}
