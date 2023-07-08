using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 500.0f;
    [SerializeField] private float friction = 1.05f;

    [SerializeField] private Vector3 targetPosition = Vector3.zero;

    private Vector3 initialPosition;

    private float speed;


    void Start()
    {
        initialPosition = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            speed = scroll * movementSpeed;
        }

        MoveCamera(speed * Time.fixedDeltaTime);
    }

    private void FixedUpdate()
    {
        speed /= friction;
    }


    private void MoveCamera(float deltaMovement){

        Camera.main.transform.RotateAround(targetPosition, Vector3.up, deltaMovement);
    }
}
