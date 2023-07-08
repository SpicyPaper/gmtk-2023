using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        float maxDistance = 12;
        rigidbody.velocity = new Vector3(
            Random.Range(-maxDistance, maxDistance),
            Random.Range(10, maxDistance),
            Random.Range(-maxDistance, maxDistance)
        );
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < -20){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Oil Spill"))
        {
            other.GetComponent<OilSpill>().SetOnFire();
        }

    }
}
