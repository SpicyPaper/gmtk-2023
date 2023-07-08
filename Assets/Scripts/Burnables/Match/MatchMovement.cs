using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchMovement : BurnableMovement
{
    
    [SerializeField] private float timePerZig = 1.0f;

    private float currentTimeInZig = 0.0f;

    private Vector3 zigDirection = new Vector3(0, 0, 0);



    
    void FixedUpdate()
    {
        /* zig zag please */
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            burnable.GetSpeed() * Time.deltaTime
        );

        if (transform.position == targetPosition)
        {
            targetPosition = new Vector3(
                Random.Range(-5.0f, 5.0f),
                Random.Range(-5.0f, 5.0f),
                Random.Range(-5.0f, 5.0f)
            );
        }
    }

}
