using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnableMovement : MonoBehaviour
{
    private Burnable burnable;

    [SerializeField]
    private Vector3 targetPosition = new Vector3(0, 0, 0);

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        burnable = GetComponent<Burnable>();
        animator = GetComponent<Animator>();
        /* disable collision with Tag Burnable */
        Physics.IgnoreLayerCollision(8, 8);
        RotateToCenter();
        Animate();
    }

    void RotateToCenter()
    {
        Vector3 direction = (transform.position - targetPosition).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    void Animate()
    {
        if (animator != null)
        {
            animator.speed = 2.5f * burnable.GetSpeed();
        }
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


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Fire")
        {
            GameController.instance.BurnableReachedFire(burnable);
        }
    }

}
