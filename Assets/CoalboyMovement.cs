using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalboyMovement : BurnableMovement
{
    // Start is called before the first frame update

    // Update is called once per frame

    protected override void ChildMovementSetup()
    {
        Vector3 direction = targetPosition - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
        animator.speed = 1;
        base.ChildMovementSetup();
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                burnable.GetSpeed() * Time.deltaTime
            );
    }
}
