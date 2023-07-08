using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burnable : MonoBehaviour
{

    [SerializeField] private float burnPower = 1.0f;
    // Start is called before the first frame update

    [SerializeField] private int initialHealth = 1;

    [SerializeField] private float speed = 1;

    private int health;

    private Animator animator;

    void Start()
    {
        health = initialHealth;

        animator = GetComponent<Animator>();
    }

    public float GetBurnPower()
    {
        return burnPower;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public bool TakeDamage(int damage)
    {
        health -= damage;

        /* if health is below 0 the gameobject will be destroyed */
        bool isDead = health <= 0;
        if (isDead)
        {
            if (animator != null)
            {
                animator.speed = 2;
                animator.SetTrigger("OnDie");
                speed = 0;
                Destroy(gameObject, 1.5f);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        return isDead;
    }

    public void OnDestroy(){
        Destroy(gameObject.transform.parent.gameObject);
    }
}
