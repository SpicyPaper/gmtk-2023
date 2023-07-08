using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnableCollider : MonoBehaviour
{
    private Burnable burnable;

    // Start is called before the first frame update
    void Start()
    {
        burnable = transform.parent.GetComponent<Burnable>();

        /* disable collision with Tag Burnable */
        Physics.IgnoreLayerCollision(8, 8);

    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Fire")
        {
            Debug.Log("Burnable reached fire");
            GameController.instance.BurnableReachedFire(burnable);
        }
    }
}
