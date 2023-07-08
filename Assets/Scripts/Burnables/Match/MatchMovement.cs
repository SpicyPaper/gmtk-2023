using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchMovement : BurnableMovement
{
    
    [SerializeField] private float timePerZig = 1.0f;

    private float currentTimeInZig = 0.0f;

    [SerializeField] private float zigAngle = 160f;

    
    void FixedUpdate()
    {
        currentTimeInZig += Time.deltaTime;

        if (currentTimeInZig > timePerZig)
        {
            currentTimeInZig = 0.0f;
            zigAngle = -zigAngle;
        }


        Vector3 unnormalizedDistance = targetPosition - transform.position;

        /* zig zag please */
        transform.position = Vector3.MoveTowards(
            transform.position,
            // target position is a cominbination of the acutal target position and the zig angle,
            // which is an angle in the xz plane
            Quaternion.Euler(0, zigAngle, 0) * unnormalizedDistance,
            burnable.GetSpeed() * Time.deltaTime
        );

    }

}
