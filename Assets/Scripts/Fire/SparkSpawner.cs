using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject sparkPrefab;

    [SerializeField]
    private float spawnInterval = 5.0f;
    
    private float nextSpawnInterval = 0.0f;
    private float lastSpawnTime = 0.0f;

    private Vector3 origin;

    // Start is called before the first frame update
    void Start()
    {
        origin = new Vector3(0.0f, 1.0f, 0.0f);
    }

    void Update()
    {
        lastSpawnTime += Time.deltaTime;
        if (lastSpawnTime >= nextSpawnInterval){
            SpawnSpark();
            lastSpawnTime = 0.0f;
            nextSpawnInterval = Random.Range(spawnInterval*0.8f, spawnInterval*1.4f);
        }
    }

    private void SpawnSpark(){
        GameObject spark = Instantiate(sparkPrefab, origin, Quaternion.identity);
        Debug.Log("Spark Created.");
    }
}
