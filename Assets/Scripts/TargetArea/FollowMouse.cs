using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    Plane plane = new Plane(Vector3.up, 0);

    [SerializeField] private Light spotlight;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance)){
            Vector3 worldPosition = ray.GetPoint(distance);
            transform.position = worldPosition;

            Vector3 spotlightPosition = ray.GetPoint(distance / 4);
            spotlight.transform.position = spotlightPosition;

            spotlight.transform.LookAt(gameObject.transform);
        }
    }
}
