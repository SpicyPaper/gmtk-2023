using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 500.0f;

    [SerializeField] private Vector3 targetPosition = Vector3.zero;

    private Vector3 initialPosition;

    private float distanceFromTarget;


    void Start()
    {
        initialPosition = transform.position;
        distanceFromTarget = Vector3.Distance(initialPosition, targetPosition);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        /* use mouse scroll to zoom in or out */
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        /* get the current camera position */
        MoveCamera(scroll * movementSpeed * Time.fixedDeltaTime);
    }


    private void MoveCamera(float deltaMovement){
        Camera.main.transform.RotateAround(targetPosition, Vector3.up, deltaMovement);
    }
}
