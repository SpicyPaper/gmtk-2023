using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnableMovement : MonoBehaviour
{

    [SerializeField] private float speed = 5f;

    [SerializeField] private Vector3 targetPosition = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
