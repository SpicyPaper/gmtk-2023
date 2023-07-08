using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetrolCanMovement : BurnableMovement
{
    [SerializeField]
    private float angleAtStep = 2f;

    [SerializeField]
    private GameObject oilSpillPrefab;

    [SerializeField]
    private float oilSpillInterval = 0;

    private float oilSpillTimer = 0f;

    private Vector3 currentDirection;

    private GameObject oilSpillParent;

    protected override void ChildMovementSetup()
    {
        // multiply angle by -1 at random
        // get random from 0 to 1 if greater than 0.5 multiply by -1
        if (Random.Range(0f, 1f) > 0.5f)
        {
            angleAtStep *= -1;
        }

        // set oil spill parent with the Tag "Oil Spill Holder"
        oilSpillParent = GameObject.FindGameObjectWithTag("Oil Spill Holder");
    }

    void Update()
    {
        if (burnable.GetSpeed() > 0)
        {
            oilSpillTimer += Time.deltaTime;
            if (oilSpillTimer > oilSpillInterval)
            {
                oilSpillTimer = 0f;

                GameObject oilSpill = Instantiate(
                    oilSpillPrefab,
                    transform.position,
                    Quaternion.identity,
                    oilSpillParent.transform
                );
                // set oil spill x and z scale to a random value between 0.5 and 1.5
                float randomScale = Random.Range(1.5f, 2f);
                oilSpill.transform.localScale = new Vector3(
                    randomScale,
                    oilSpill.transform.localScale.y,
                    randomScale
                );
            }
        }
    }

    void FixedUpdate()
    {
        if (burnable.GetSpeed() > 0)
        {

            Vector3 oldPosition = transform.position;

            transform.RotateAround(targetPosition, Vector3.up, angleAtStep);

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
}
