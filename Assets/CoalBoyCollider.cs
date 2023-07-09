using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalBoyCollider : BurnableCollider
{

    [SerializeField] private GameObject coalBoyBody;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Fire")
        {
            GameController.instance.BurnableReachedFire(burnable);

        }
    }

    protected override void OnTriggerEnter(Collider other)
    {

        // call base class
        base.OnTriggerEnter(other);

        if (other.gameObject.tag == "Oil Spill")
        {
            OilSpill oilSpill = other.GetComponent<OilSpill>();


            if (oilSpill != null && oilSpill.IsBurning())
            {                
                // invoke SetOnFire() on oilSpill with delay
                this.Invoke("setMyselfOnFire", 0.2f);
            }

        }

    }

    void setMyselfOnFire()
    {
        coalBoyBody.GetComponent<SkinnedMeshRenderer>().material.color = Color.red;
        burnable.SetSpeed(burnable.GetSpeed() * 2);
    }

}
