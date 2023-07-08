using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PetrolCanCollider : BurnableCollider
{

    [SerializeField] private GameObject oilSpillPrefab;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Fire")
        {
            GameController.instance.BurnableReachedFire(burnable);

            GameObject oilSpill = Instantiate(
                    oilSpillPrefab,
                    transform.position,
                    Quaternion.identity
            );

            // set oil spill x and z scale to a random value between 0.5 and 1.5
            float randomScale = 3f;
            oilSpill.transform.localScale = new Vector3(
                randomScale,
                oilSpill.transform.localScale.y,
                randomScale
            );
        }
    }
}
