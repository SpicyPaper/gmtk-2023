using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnableCollider : MonoBehaviour
{
    protected  Burnable burnable;

    private float initialSpeed;

    // Start is called before the first frame update
    void Start()
    {
        burnable = GetComponent<Burnable>();
        initialSpeed = burnable.GetSpeed();

        /* disable collision with Tag Burnable */
        Physics.IgnoreLayerCollision(8, 8);


    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Fire")
        {
            GameController.instance.BurnableReachedFire(burnable);
        }


    }

    virtual protected void OnTriggerEnter(Collider other)
    {
        // collision with puddle
        if (other.gameObject.tag == "Puddle")
        {
            burnable.SetSpeed(initialSpeed/2);
        }
    }

    virtual protected void OnTriggerExit(Collider other)
    {
        // collision with puddle
        if (other.gameObject.tag == "Puddle")
        {
            burnable.SetSpeed(initialSpeed);
        }
    }
}
