using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchMovement : BurnableMovement
{
    
    [SerializeField] private float timePerZig = 1.0f;

    private float currentTimeInZig = 0.0f;

    private Vector3 zigAbsDelta = new Vector3(10, 0, 10);

    
    void FixedUpdate()
    {
        currentTimeInZig += Time.deltaTime;

        if (currentTimeInZig > timePerZig)
        {
            currentTimeInZig = 0.0f;
            zigAbsDelta = -zigAbsDelta;
        }

        /* zig zag please */
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition + zigAbsDelta,
            burnable.GetSpeed() * Time.deltaTime
        );

    }

}
