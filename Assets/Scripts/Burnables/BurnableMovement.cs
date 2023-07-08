using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnableMovement : MonoBehaviour
{
    private Burnable burnable;

    [SerializeField]
    private Vector3 targetPosition = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        burnable = GetComponent<Burnable>();
        /* disable collision with Tag Burnable */
        Physics.IgnoreLayerCollision(8, 8);

    }

    // Update is called once per frame
    void Update() { }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            burnable.GetSpeed() * Time.deltaTime
        );
    }

}
