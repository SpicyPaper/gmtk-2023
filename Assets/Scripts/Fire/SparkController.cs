using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkController : MonoBehaviour
{
    [SerializeField] private GameObject oilSpillPrefab;

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

    void Update()
    {
        if (gameObject.transform.position.y <= 0){
            GameObject oilOnFire = Instantiate(oilSpillPrefab, gameObject.transform.position, Quaternion.identity);
        }
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
