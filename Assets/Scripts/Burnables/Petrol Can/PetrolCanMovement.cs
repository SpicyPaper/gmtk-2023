using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetrolCanMovement : BurnableMovement
{
    [SerializeField]
    private float angleAtStep = 1f;

    private Vector3 currentDirection;

    void FixedUpdate()
    {

        Vector3 oldPosition = transform.position;

        transform.RotateAround(
            targetPosition,
            Vector3.up,
            angleAtStep
        );
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            burnable.GetSpeed() * Time.deltaTime
        );

        Vector3 newPosition = transform.position;

        Vector3 direction = (oldPosition - newPosition).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
