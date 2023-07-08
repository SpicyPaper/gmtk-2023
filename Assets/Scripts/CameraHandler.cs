using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{

    [SerializeField] private float zoomSpeed = 100.0f;

    [SerializeField] private float minY = 10.0f;

    [SerializeField] private float maxY = 100.0f;

    

    // Update is called once per frame
    void FixedUpdate()
    {
        /* use mouse scroll to zoom in or out */
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        /* get the current camera position */
        Vector3 position = Camera.main.transform.position;
        /* change the z position of the camera */
        position.y -= scroll * zoomSpeed * Time.deltaTime;
        /* set the camera position to the new position */

        position.y = Mathf.Clamp(position.y, minY, maxY);

        Camera.main.transform.position = position;

    }
}
