using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalBoyCollider : BurnableCollider
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Fire")
        {
            GameController.instance.BurnableReachedFire(burnable);

        }
    }
}
