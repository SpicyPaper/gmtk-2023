using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.velocity = new Vector3(
            Random.Range(-10,10),
            Random.Range(10,20),
            Random.Range(-10,10)
        );
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < -20){
            Destroy(gameObject);
        }
    }
}
